using DG.Tweening;
using UnityEngine;

public class TutorialHandAnimation : MonoBehaviour
{
    Tweener tutorialTween;

    void OnEnable()
    {
        PlayTutorialHandAnim();
    }

    private void PlayTutorialHandAnim()
    {
        tutorialTween = transform.DOLocalMoveX(175f, 1f).From(-175f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        tutorialTween.Kill();
    }
}
