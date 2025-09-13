using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 m_movementVector;
    [SerializeField] float m_speed;
    
    Vector3 m_startPos;
    Vector3 m_endPos;
    float movementFactor;

    void Start()
    {
        m_startPos = transform.position;
        m_endPos = m_startPos + m_movementVector;
    }

    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * m_speed, 1f);
        transform.position = Vector3.Lerp(m_startPos, m_endPos, movementFactor);
    }

}
