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

    [SerializeField] float m_delayLoadScene = 2f;

    Coroutine m_crtFinish = null;
    bool m_isCollide = true;


    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            m_isCollide = !m_isCollide;
        }
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
    IEnumerator ReloadCurScene()
    {
        yield return new WaitForSeconds(m_delayLoadScene);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CrashProcess()
    {
        m_movementHandler.enabled = false;
        m_isCollide = false;

        //! Play particle, audio
        m_audioSource.PlayOneShot(m_crashAC);
        m_explosionPs.Play();


        if (m_crtFinish != null)
        {
            StopCoroutine(m_crtFinish);
            m_crtFinish = null;
        }
        
        // Invoke("ReloadCurScene", m_delayLoadScene);
        StartCoroutine(ReloadCurScene());

        Debug.Log("Wait Reload Cur Scene");
    }




    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(m_delayLoadScene);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(nextSceneIndex);
    }

    void Finish()
    {
        if (m_crtFinish != null)
            return;
            
        m_movementHandler.enabled = false;

        //! Play particle, audio
        m_audioSource.PlayOneShot(m_finishAC);
        m_successPs.Play();


        // Invoke("LoadNextScene", m_delayLoadScene);
        m_crtFinish = StartCoroutine(LoadNextScene());

        Debug.Log("Wait Load Next Scene");
    }
}
