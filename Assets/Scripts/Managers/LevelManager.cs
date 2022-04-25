using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] List<GameObject> levels = new List<GameObject>();
    private int currentLevel = 0;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            currentLevel = PlayerPrefs.GetInt("Level");
        }
        else
        {
            PlayerPrefs.SetInt("Level", 0);
        }
        currentLevel = 2;
        LoadLevelAtIndex(currentLevel);
        if (currentLevel > 1)
        {
            DOTween.Sequence().SetDelay(0.2f).OnComplete(() => LevelGenerator.Instance.GenerateLevel());
        }
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        currentLevel++;
        currentLevel = Mathf.Clamp(currentLevel, 0, levels.Count-1);
        PlayerPrefs.SetInt("Level", currentLevel);
        SceneManager.LoadScene(0);
    }

    public void LoadLevelAtIndex(int index)
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].SetActive(i == index);
        }
    }
}
