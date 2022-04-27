using UnityEngine;

public class WeaponBazooka : Weapon
{
    Transform prevTarget;
    [SerializeField] int ammoCount;

    protected override void Update()
    {
        if (detector.DetectedTarget(out Transform target))
        {
            if (prevTarget != target)
            {
                ShootWeapon();
                prevTarget = target;
            }
        }
        else
        {
            shootTimer = 0f;
        }
    }

    protected override void ShootWeapon()
    {
        if(ammoCount > 0)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootCD)
            {
                foreach (var shooter in shooters)
                {
                    shooter.Shoot(bulletType);
                    ammoCount--;
                }
                PlayRecoilAnim();
                shootTimer = 0f;
            }
        }
        else
        {
            //stop finish sequence
        }
    }

    public void AddAmmo(int amount)
    {
        ammoCount += amount;
    }
}
