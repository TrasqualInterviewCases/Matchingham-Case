using DG.Tweening;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Action OnPlayerFailed;

    private bool isFailed;

    public void Fail()
    {
        if (isFailed) return;
        isFailed = true;
        OnPlayerFailed?.Invoke();
        DOTween.Sequence().SetDelay(2f).OnComplete(() => GameManager.Instance.LoseGame());
    }
}
