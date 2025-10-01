using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float delayLoadScene = 2f;
    ScoreKeeper m_scoreKeeper;


    public enum eScene
    {
        Menu = 0,
        Level1,
        Level2,
        Level3
    }

    Coroutine m_crtFinish = null;

    void Awake()
    {
        m_scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    public bool IsFinish()
    {
        return m_crtFinish != null;
    }

   IEnumerator loadScene(eScene scene)
    {
        if(scene != eScene.Menu)
            yield return new WaitForSeconds(delayLoadScene);
            
        SceneManager.LoadScene((int)scene);
    }

    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if(nextSceneIndex == 1)
            m_scoreKeeper.resetScore();

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(0);
            
        m_crtFinish = StartCoroutine(loadScene((eScene)nextSceneIndex));
            
    }

    public void ReloadCurScene()
    {
        if (m_crtFinish != null)
            StopCoroutine(m_crtFinish);

        StartCoroutine(loadScene((eScene)SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadMenu()
    {
        StartCoroutine(loadScene(eScene.Menu));
    }

 

    public void Quit()
    {
        Application.Quit();
    }
}
