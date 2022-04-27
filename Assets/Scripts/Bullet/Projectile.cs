using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    [Header("Data")]
    [SerializeField] int damage = 1;
    [SerializeField] float bulletSpeed = 50f;
    [SerializeField] float bulletRequeueTimer = 2f;
    [SerializeField] PoolableObjectType type;
    private bool isShot;

    [Header("Components")]
    [SerializeField] Rigidbody rb;
    [SerializeField] TrailRenderer tr;

    IEnumerator requeCo;

    private void Update()
    {
        if(isShot)
        rb.MovePosition(transform.position + transform.forward * Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ObstacleBase obstacle))
        {
            if (requeCo != null)
            {
                StopCoroutine(requeCo);
            }
            requeCo = RequeueBullet(0f);
            StartCoroutine(requeCo);
        }

        if (other.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(damage);
            if (requeCo != null)
            {
                StopCoroutine(requeCo);
            }
            requeCo = RequeueBullet(0f);
            StartCoroutine(requeCo);
        }
    }

    IEnumerator RequeueBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        isShot = false;
        tr.enabled = false;
        ObjectPooler.Instance.RequeuePiece(gameObject);
    }

    public void Shoot()
    {
        isShot = true;
        tr.enabled = true;
        requeCo = RequeueBullet(bulletRequeueTimer);
        StartCoroutine(requeCo);
    }

    PoolableObjectType IPoolable.GetType()
    {
        return type;
    }
}
