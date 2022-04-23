using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] protected float shootCD = 0.5f;
    protected float shootTimer = 0f;

    [Header("Visuals")]
    [SerializeField] protected PoolableObjectType bulletType;

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

    protected void ShootWeapon()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCD)
        {
            foreach (var shooter in shooters)
            {
                shooter.Shoot(bulletType);
            }
            shootTimer = 0f;
        }
    }

    public void GetIKTargets(out Transform leftTarget, out Transform rightTarget)
    {
        leftTarget = targetForLeft;
        rightTarget = targetForRight;
    }
}
