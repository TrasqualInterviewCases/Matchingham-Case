using UnityEngine;

[System.Serializable]
public class ObjectPool : MonoBehaviour
{
    public PoolableObjectType type;
    public GameObject prefab;
    public int poolSize = 30;
}
