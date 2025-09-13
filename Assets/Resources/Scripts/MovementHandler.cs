using UnityEngine;
using UnityEngine.InputSystem;

public class MovementHandler : MonoBehaviour
{


    [SerializeField] InputAction m_thrustIA;
    [SerializeField] InputAction m_rotateIA;

    [SerializeField] Rigidbody m_rb;
    [SerializeField] AudioSource m_audioSource;

    [SerializeField] AudioClip m_mainEngineAC;

    [SerializeField] float m_thrustStrength = 1000f;
    [SerializeField] float m_rotateStrength = 50f;
    [SerializeField] ParticleSystem m_mainEnginePS;
    [SerializeField] ParticleSystem m_leftEnginePS;
    [SerializeField] ParticleSystem m_rightEnginePS;


    void OnEnable()
    {
        m_thrustIA.Enable();
        m_rotateIA.Enable();
    }



    void FixedUpdate()
    {
        Thrust();
        Rotate();
    }


    void Thrust()
    {

        //! Input Action(Arrow keys, ADWS keys)
        if (m_thrustIA.IsPressed())
        {
            m_rb.AddRelativeForce(Vector3.up * (m_thrustStrength * Time.fixedDeltaTime));

            if (!m_audioSource.isPlaying)
                m_audioSource.PlayOneShot(m_mainEngineAC);

            m_mainEnginePS.Play();

        }
        else
        {
            m_audioSource.Stop();
            m_mainEnginePS.Stop();
        }
        

        
        //! Joytick,....
        }


    void Rotate()
    {
        //! Input Action(Arrow keys, ADWS keys)
        if (m_rotateIA.IsPressed())
        {
            float valueRotate = m_rotateIA.ReadValue<float>();
            transform.Rotate(Vector3.back * (valueRotate * Time.fixedDeltaTime * m_rotateStrength));

            if (valueRotate > 0)
            {
                m_leftEnginePS.Stop();
                m_rightEnginePS.Play();
                
            }
            else if (valueRotate < 0)
            {
                m_rightEnginePS.Stop();
                m_leftEnginePS.Play();
            }    
                
        }
        else
        {
            m_leftEnginePS.Stop();
            m_rightEnginePS.Stop();
        }


        //! Joytick,....
    }

}
