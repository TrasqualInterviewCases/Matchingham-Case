using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [Header("Data")]
    [SerializeField] int damage = 1;
    [SerializeField] float bulletSpeed = 50f;
    [SerializeField] float bulletRequeueTimer = 2f;
    [SerializeField] PoolableObjectType type;


    [Header("Components")]
    [SerializeField] Rigidbody rb;

    IEnumerator requeCo;

    private void OnEnable()
    {
        requeCo = RequeueBullet(bulletRequeueTimer);
        StartCoroutine(requeCo);
    }

    private void Update()
    {
        rb.MovePosition(transform.position + transform.forward * Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(damage);

            gameObject.SetActive(false);
        }
    }

    IEnumerator RequeueBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        ObjectPooler.Instance.RequeuePiece(gameObject);
    }

    PoolableObjectType IPoolable.GetType()
    {
        return type;
    }

    private void OnDisable()
    {
        if (requeCo != null)
        {
            StopCoroutine(requeCo);
        }
        requeCo = RequeueBullet(0f);
    }
}
