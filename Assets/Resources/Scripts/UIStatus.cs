using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIStatus : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI m_UIScore;
    ScoreKeeper m_scoreKeeper;
    void Awake()
    {
        m_scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    void Update()
    {
        m_UIScore.text = m_scoreKeeper.getScore().ToString("000000");
    }

}
