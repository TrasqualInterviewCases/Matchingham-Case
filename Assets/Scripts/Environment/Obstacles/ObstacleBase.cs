using DG.Tweening;
using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    [SerializeField] Vector3 moveEndPos;
    [SerializeField] float moveDuration;

    public bool isMoving;

    Tweener obstacleMoveTween;

    private void OnEnable()
    {
        if (isMoving)
        {
            Move();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out PlayerController player))
        {
            player.Fail();
        }  
    }


    protected virtual void Move() 
    {
        var localEndPos = transform.TransformPoint(moveEndPos);
        obstacleMoveTween = transform.DOMove(localEndPos, moveDuration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        if(obstacleMoveTween != null)
        obstacleMoveTween.Kill();
    }
}
