using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] MovementHandler m_movementHandler;
    [SerializeField] ParticleSystem m_explosionPs;
    [SerializeField] ParticleSystem m_successPs;
    
    [SerializeField] AudioSource m_audioSource;
    [SerializeField] AudioClip m_finishAC;
    [SerializeField] AudioClip m_crashAC;
    [SerializeField] LevelManager m_levelManager;

    bool m_isCollide = true;


    void Update()
    {
        //! Turn on/off collider game.
        if (Keyboard.current.tKey.wasPressedThisFrame)
            m_isCollide = !m_isCollide;

        //! Escapte to menu scene.
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            m_levelManager.LoadMenu();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!m_isCollide) return;
        
        switch (collision.gameObject.tag)
            {
                case "Obstacle":
                    CrashProcess();
                    break;
                case "Finish":
                    Finish();
                    break;
            }
    }

    void CrashProcess()
    {
        m_movementHandler.enabled = false;
        m_isCollide = false;

        //! Play particle, audio
        m_audioSource.PlayOneShot(m_crashAC);
        m_explosionPs.Play();


        // Invoke("ReloadCurScene", m_delayLoadScene);
        m_levelManager.ReloadCurScene();

    }


    void Finish()
    {
   
        if (m_levelManager.IsFinish()) return;
            
        m_movementHandler.enabled = false;

        //! Play particle, audio
        m_audioSource.PlayOneShot(m_finishAC);
        m_successPs.Play();


        // Invoke("LoadNextScene", m_delayLoadScene);

        m_levelManager.LoadNextScene();

    }
}
