using DG.Tweening;
using System.Collections;
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
            StartCoroutine(Move());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out PlayerController player))
        {
            player.Fail();
        }  
    }


    protected virtual IEnumerator Move() 
    {
        yield return new WaitForSeconds(0.2f);
        var localEndPos = transform.TransformPoint(moveEndPos);
        obstacleMoveTween = transform.DOMove(localEndPos, moveDuration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    protected void StopMovement()
    {
        if (obstacleMoveTween != null)
            obstacleMoveTween.Kill();
    }

    private void OnDisable()
    {
        StopMovement();
    }
}
