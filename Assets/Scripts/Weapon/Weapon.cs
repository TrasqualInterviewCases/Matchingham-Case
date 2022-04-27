using DG.Tweening;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] protected float shootCD = 0.5f;
    protected float shootTimer = 0f;

    [Header("Visuals")]
    [SerializeField] protected PoolableObjectType bulletType;
    [SerializeField] Vector3 gunRecoilPos;
    [SerializeField] Vector3 gunRecoilRot;

    [Header("Components")]
    [SerializeField] protected ObstacleDetector detector;
    [SerializeField] protected Shooter[] shooters;

    [Header("IKTargets")]
    [SerializeField] Transform targetForRight;
    [SerializeField] Transform targetForLeft;

    protected virtual void Update()
    {
        if (detector.DetectedTarget(out Transform target))
        {
            ShootWeapon();
        }
        else
        {
            shootTimer = 0f;
        }
    }

    protected virtual void ShootWeapon()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCD)
        {
            foreach (var shooter in shooters)
            {
                shooter.Shoot(bulletType);
            }
            PlayRecoilAnim();
            shootTimer = 0f;
        }
    }

    protected void PlayRecoilAnim()
    {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOLocalMove(gunRecoilPos, shootCD / 3f).SetEase(Ease.Linear));
        s.Join(transform.DOLocalRotate(gunRecoilRot, shootCD / 3f).SetEase(Ease.Linear));
        s.OnComplete(() => s.Rewind());
    }

    public void GetIKTargets(out Transform leftTarget, out Transform rightTarget)
    {
        leftTarget = targetForLeft;
        rightTarget = targetForRight;
    }
}
