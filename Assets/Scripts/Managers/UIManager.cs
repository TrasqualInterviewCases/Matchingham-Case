using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject startPanel;

    public void OnStartButtonClicked()
    {
        startPanel.SetActive(false);
        GameManager.Instance.StartGame();
    }
}
