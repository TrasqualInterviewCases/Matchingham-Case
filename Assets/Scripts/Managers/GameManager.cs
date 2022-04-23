using System;

public class GameManager : Singleton<GameManager>
{
    public static Action OnGameStarted;
    public static Action OnGameWon;
    public static Action OnGameFailed;

    public void WinGame()
    {
        OnGameWon?.Invoke();
    }

    public void LoseGame()
    {
        OnGameFailed?.Invoke();
    }

    public void StartGame()
    {
        OnGameStarted?.Invoke();
    }
}
