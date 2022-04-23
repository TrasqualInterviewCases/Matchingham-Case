using UnityEngine;

public class Shooter : MonoBehaviour
{
    //Add objectPooler

    public void Shoot(PoolableObjectType type)
    {
        ObjectPooler.Instance.SpawnFromPool(type, transform.position, transform.rotation);
    }
}
