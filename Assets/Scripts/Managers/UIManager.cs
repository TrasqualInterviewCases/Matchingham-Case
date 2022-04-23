using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject winPanel;

    private void ActivateLosePanel()
    {
        losePanel.SetActive(true);
    }

    private void ActivateWinPanel()
    {
        winPanel.SetActive(true);
    }

    public void OnStartButtonClicked()
    {
        startPanel.SetActive(false);
        GameManager.Instance.StartGame();
    }

    public void OnRetryButtonClicked()
    {
        LevelManager.Instance.ReloadLevel();
    }

    public void OnNextLevelButtonClicked()
    {
        LevelManager.Instance.LoadNextLevel();
    }

    private void OnEnable()
    {
        GameManager.OnGameFailed += ActivateLosePanel;
        GameManager.OnGameWon += ActivateWinPanel;
    }

    private void OnDisable()
    {
        GameManager.OnGameFailed -= ActivateLosePanel;
        GameManager.OnGameWon -= ActivateWinPanel;
    }
}
