using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] int m_score = 100;
    [SerializeField] float m_speed;
    [SerializeField] float m_delayDestroyGameObject;
    [SerializeField] ParticleSystem m_particle;
    [SerializeField] AudioClip m_audioPickup;
    [SerializeField] AudioSource m_audioSource;

    ScoreKeeper m_scoreKeeper;


    void Awake()
    {
        m_scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    void Update()
    {
        transform.Rotate(Vector3.up * (m_speed * Time.deltaTime));
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(IEPickup());
    }

    IEnumerator IEPickup()
    {
        m_particle?.Play();
        m_scoreKeeper?.collectScore(m_score);
        m_audioSource?.PlayOneShot(m_audioPickup);
        yield return new WaitForSeconds(m_delayDestroyGameObject);
        Destroy(this.gameObject);
    }
}
