using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        Debug.Log("Loading Next Level");
        SceneManager.LoadScene(0);
    }
}
