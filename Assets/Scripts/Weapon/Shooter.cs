using UnityEngine;

public class Shooter : MonoBehaviour
{
    public void Shoot(PoolableObjectType type)
    {
        var projectile = ObjectPooler.Instance.SpawnFromPool(type, transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().Shoot();
    }
}
