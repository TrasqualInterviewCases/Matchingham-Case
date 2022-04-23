using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] List<ObjectPool> pools = new List<ObjectPool>();
    Dictionary<PoolableObjectType, Queue<GameObject>> queueDictionary = new Dictionary<PoolableObjectType, Queue<GameObject>>();
    Dictionary<PoolableObjectType, ObjectPool> poolDictionary = new Dictionary<PoolableObjectType, ObjectPool>();

    public static ObjectPooler Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        foreach (var pool in pools)
        {
            Queue<GameObject> piecePool = new Queue<GameObject>();
            var poolParent = new GameObject(pool.type.ToString() + " pool");
            poolParent.transform.SetParent(transform);
            for (int i = 0; i < pool.poolSize; i++)
            {
                var spawnedPiece = Instantiate(pool.prefab, poolParent.transform);
                spawnedPiece.SetActive(false);
                piecePool.Enqueue(spawnedPiece);
            }

            poolDictionary.Add(pool.type, pool);
            queueDictionary.Add(pool.type, piecePool);
        }
    }

    public GameObject SpawnFromPool(PoolableObjectType type, Vector3 pos, Quaternion rot)
    {
        GameObject piece;
        if (queueDictionary[type].Count <= 0)
        {
            piece = Instantiate(poolDictionary[type].prefab);
            piece.SetActive(false);
            queueDictionary[type].Enqueue(piece);
        }

        piece = queueDictionary[type].Dequeue();


        piece.transform.position = pos;
        piece.transform.rotation = rot;
        piece.SetActive(true);

        if (queueDictionary[type].Count > poolDictionary[type].poolSize)
        {
            for (int i = 0; i < queueDictionary[type].Count - poolDictionary[type].poolSize; i++)
            {
                var extraPiece = queueDictionary[type].Dequeue();
                Destroy(extraPiece);
            }
        }

        return piece.GetComponent<GameObject>();
    }

    public void RequeuePiece(GameObject piece)
    {
        piece.transform.position = transform.position;
        piece.SetActive(false);
        queueDictionary[piece.GetComponent<IPoolable>().GetType()].Enqueue(piece);
    }
}
