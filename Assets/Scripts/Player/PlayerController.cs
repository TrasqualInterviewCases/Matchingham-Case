using DG.Tweening;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Action OnPlayerFailed;
    public static Action OnPlayerFinished;

    private bool isFailed;

    public void Fail()
    {
        if (isFailed) return;
        isFailed = true;
        OnPlayerFailed?.Invoke();
        GetComponentInChildren<Animator>().applyRootMotion = true;
        DOTween.Sequence().SetDelay(2f).OnComplete(() => GameManager.Instance.LoseGame());
    }

    public void Finish()
    {
        GameManager.Instance.WinGame();
        OnPlayerFinished?.Invoke();
    }
}
