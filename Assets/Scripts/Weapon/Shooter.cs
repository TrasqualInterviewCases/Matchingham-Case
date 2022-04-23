using UnityEngine;

public class Shooter : MonoBehaviour
{
    public void Shoot(PoolableObjectType type)
    {
        ObjectPooler.Instance.SpawnFromPool(type, transform.position, transform.rotation);
    }
}
