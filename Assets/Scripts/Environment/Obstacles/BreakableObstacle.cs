using TMPro;
using UnityEngine;

public class BreakableObstacle : ObstacleBase, IDamagable
{
    [SerializeField] Transform model;
    [SerializeField] Transform fracturedModel;
    [SerializeField] Collider col;

    [SerializeField] TMP_Text healthText;
    [SerializeField] int maxHealth = 10;
    int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        SetHealthText();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            BreakDown();
            currentHealth = 0;
        }
        DisplayFractures();
        SetHealthText();
    }

    private void DisplayFractures()
    {
        if (currentHealth == Mathf.RoundToInt(maxHealth * 0.6f))
        {
            model.gameObject.SetActive(false);
            fracturedModel.gameObject.SetActive(true);
        }
        else if(currentHealth == Mathf.RoundToInt(maxHealth * 0.3f))
        {
            foreach (Transform piece in fracturedModel)
            {
                piece.transform.position += Random.insideUnitSphere * 0.05f;
            }
        }
    }

    private void SetHealthText()
    {
        healthText.SetText(currentHealth.ToString());
    }

    private void BreakDown()
    {
        col.enabled = false;
        healthText.gameObject.SetActive(false);
        if (fracturedModel.childCount > 0)
        {
            foreach (Transform child in fracturedModel)
            {
                var childRB = child.gameObject.AddComponent<Rigidbody>();
                child.gameObject.AddComponent<BoxCollider>();
                childRB.AddExplosionForce(2000f, fracturedModel.position, 10f, 1f);
                child.SetParent(null);
                Destroy(child.gameObject, 5f);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
