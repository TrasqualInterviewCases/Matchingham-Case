using System;
using UnityEngine;

public class WeaponBazooka : Weapon
{
    public Action OnOutOfAmmo;

    [SerializeField] int ammoCount;

    protected override void Update()
    {
        if (detector.DetectedTarget(out Transform target))
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootCD)
            {
                if (target.TryGetComponent(out FinishWall finishWall))
                {
                    if (!finishWall.IsShotAt)
                    {
                        ShootWeapon();
                        shootTimer = 0f;
                        finishWall.IsShotAt = true;
                    }
                }
            }
        }
    }

    protected override void ShootWeapon()
    {
        if (ammoCount > 0)
        {
            foreach (var shooter in shooters)
            {
                shooter.Shoot(bulletType);
                ammoCount--;
            }
            PlayRecoilAnim();
        }
        else
        {
            OnOutOfAmmo?.Invoke();
        }
    }

    public void AddAmmo(int amount)
    {
        ammoCount += amount;
        ammoCount = Mathf.Clamp(ammoCount, 0, 20);
    }
}
