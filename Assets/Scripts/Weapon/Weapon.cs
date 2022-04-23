using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] protected float shootCD = 0.5f;
    protected float shootTimer = 0f;

    [Header("Visuals")]
    [SerializeField] protected Bullet bulletPrefab;

    [Header("Components")]
    [SerializeField] ObstacleDetector detector;
    [SerializeField] Shooter[] shooters;

    bool canDetect = true;

    private void Update()
    {
        if (canDetect)
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
    }

    protected void ShootWeapon()
    {
        shootTimer += Time.deltaTime;
        if(shootTimer >= shootCD)
        {
            foreach (var shooter in shooters)
            {
                shooter.Shoot(bulletPrefab);
            }
            shootTimer = 0f;
        }
    }
}
