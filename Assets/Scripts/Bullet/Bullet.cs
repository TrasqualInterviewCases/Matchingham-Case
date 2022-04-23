using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] int damage = 1;
    [SerializeField] float bulletSpeed = 50f;
    [SerializeField] float bulletRequeueTimer = 3f;


    [Header("Components")]
    [SerializeField] Rigidbody rb;

    private void Start()
    {
        StartCoroutine(RequeueBullet());
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
            //Refactor with pooler
            Destroy(gameObject);
        }
    }

    IEnumerator RequeueBullet()
    {
        yield return new WaitForSeconds(bulletRequeueTimer);
        Destroy(gameObject);
    }

    public void SetPooler(/*objectpooler*/)
    {

    }
}
