using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject winPanel;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text collectedScoreText;

    PointCollector pointCollector;

    private void Awake()
    {
        pointCollector = PointCollector.Instance;
    }

    private void ActivateLosePanel()
    {
        gamePanel.SetActive(false);
        losePanel.SetActive(true);
    }

    private void ActivateWinPanel()
    {
        gamePanel.SetActive(false);
        winPanel.SetActive(true);
        SetScoreText();
    }

    public void OnStartButtonClicked()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
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

    public void SetCollectedStoreText()
    {
        collectedScoreText.SetText($"{pointCollector.GetCollectedPoints()}");
    }

    private void SetScoreText()
    {
        StartCoroutine(SetScoreTextCo());
    }

    private IEnumerator SetScoreTextCo()
    {
        var currentScore = pointCollector.GetTotalPoints();
        while(currentScore < pointCollector.GetUpdatedPoints())
        {
            currentScore++;
            scoreText.SetText($"Score: {currentScore}");
            yield return new WaitForSeconds(0.1f);
        }
        pointCollector.SetTotalCoins();
    }

    private void OnEnable()
    {
        PointCollector.OnPointsEarned += SetCollectedStoreText;
        GameManager.OnGameFailed += ActivateLosePanel;
        GameManager.OnGameWon += ActivateWinPanel;
    }

    private void OnDisable()
    {
        PointCollector.OnPointsEarned -= SetCollectedStoreText;
        GameManager.OnGameFailed -= ActivateLosePanel;
        GameManager.OnGameWon -= ActivateWinPanel;
    }
}
