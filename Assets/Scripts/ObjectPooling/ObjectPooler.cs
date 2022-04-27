using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    [SerializeField] List<ObjectPool> pools = new List<ObjectPool>();
    Dictionary<PoolableObjectType, Queue<GameObject>> queueDictionary = new Dictionary<PoolableObjectType, Queue<GameObject>>();
    Dictionary<PoolableObjectType, ObjectPool> poolDictionary = new Dictionary<PoolableObjectType, ObjectPool>();
    Dictionary<PoolableObjectType, Transform> poolParents = new Dictionary<PoolableObjectType, Transform>();

    private void Awake()
    {
        foreach (var pool in pools)
        {
            Queue<GameObject> piecePool = new Queue<GameObject>();
            var poolParent = new GameObject(pool.type.ToString() + " pool");
            poolParent.transform.SetParent(transform);
            poolParents[pool.type] = poolParent.transform;
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
            piece.transform.SetParent(poolParents[type].transform);
            piece.SetActive(false);
            queueDictionary[type].Enqueue(piece);
        }

        piece = queueDictionary[type].Dequeue();


        piece.transform.position = pos;
        piece.transform.rotation = rot;
        piece.transform.SetParent(null);
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
        var pieceType = piece.GetComponent<IPoolable>().GetType();
        piece.transform.position = transform.position;
        piece.transform.SetParent(poolParents[pieceType].transform);
        queueDictionary[pieceType].Enqueue(piece);
        piece.SetActive(false);
    }
}
