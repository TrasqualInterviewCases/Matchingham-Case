using UnityEngine;

public class ProjectileRocket : Projectile
{
    [SerializeField] int amount = 1;

    public int Amount { get { return amount; } }

    public void GetCollected()
    {
        ObjectPooler.Instance.RequeuePiece(gameObject);
    }
}
