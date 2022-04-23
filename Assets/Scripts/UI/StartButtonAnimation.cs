using DG.Tweening;
using UnityEngine;

public class StartButtonAnimation : MonoBehaviour
{
    Tweener startButtonTween;

    void OnEnable()
    {
        StartButtonAnim();
    }

    private void StartButtonAnim()
    {
        startButtonTween = transform.DOScale(Vector3.one * 1.1f, 0.5f).From(Vector3.one * 0.8f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        startButtonTween.Kill();
    }
}
