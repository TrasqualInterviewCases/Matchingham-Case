
using UnityEngine;

public class BreakableObstacle : ObstacleBase, IDamagable
{
    [SerializeField] Transform model;
    [SerializeField] Collider col;

    [SerializeField] int maxHealth = 10;
    int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            BreakDown();
            currentHealth = 0;
        }
    }

    private void BreakDown()
    {
        col.enabled = false;
        if (model.childCount > 0)
        {
            foreach (Transform child in model)
            {
                child.gameObject.AddComponent<Rigidbody>();
                child.gameObject.AddComponent<BoxCollider>();
                Destroy(child.gameObject, 5f);
            }
        }
        else
        {
            Destroy(model.gameObject);
        }
    }
}
