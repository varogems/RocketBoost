using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] int m_score;
    
    public static ScoreKeeper m_instance {get; private set;}



    private void Awake() 
    {
        //! Deprecated
        // if (FindObjectsOfType(this.GetType()).Length > 1)
        
        if(FindObjectsByType<ScoreKeeper>(FindObjectsSortMode.None).Length > 1)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
            return;
        }
        else
        {
            m_score = 0;
            m_instance = this;

            Debug.Log("Init ScoreKeeper success!");

            DontDestroyOnLoad(this.gameObject);

        }

    }

    public int getScore()
    {
        return m_score;
    }


    public void collectScore(int score)
    {
        m_score += score;
        Mathf.Clamp(m_score, 0, int.MaxValue);
    }

    public void resetScore()
    {
        m_score = 0;
    }

}
