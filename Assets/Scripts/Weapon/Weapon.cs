using DG.Tweening;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] float shootCD = 0.5f;
    float shootTimer = 0f;

    [Header("Visuals")]
    [SerializeField] PoolableObjectType bulletType;
    [SerializeField] Vector3 gunRecoilPos;
    [SerializeField] Vector3 gunRecoilRot;

    [Header("Components")]
    [SerializeField] ObstacleDetector detector;
    [SerializeField] Shooter[] shooters;

    [Header("IKTargets")]
    [SerializeField] Transform targetForRight;
    [SerializeField] Transform targetForLeft;

    private void Update()
    {
        if (detector.DetectedTarget())
        {
            ShootWeapon();
        }
        else
        {
            shootTimer = 0f;
        }
    }

    private void ShootWeapon()
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

    private void PlayRecoilAnim()
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
