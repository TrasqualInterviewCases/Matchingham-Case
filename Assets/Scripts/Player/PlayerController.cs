using DG.Tweening;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Action OnPlayerFailed;

    public void Fail()
    {
        OnPlayerFailed?.Invoke();
        DOTween.Sequence().SetDelay(2f).OnComplete(() => GameManager.Instance.LoseGame());
    }
}
