using TMPro;
using UnityEngine;

public class FinishWall : MonoBehaviour, IDamagable
{
    [SerializeField] GameObject wallModel;
    [SerializeField] Transform fracturedWallModel;
    [SerializeField] Collider col;
    [SerializeField] TMP_Text text;

    [SerializeField] int multiplier = 1;

    public bool IsShotAt { get; set; }

    public void TakeDamage(int amount)
    {
        BreakDown();
    }

    private void BreakDown()
    {
        col.enabled = false;
        wallModel.SetActive(false);
        fracturedWallModel.gameObject.SetActive(true);
        PointCollector.Instance.SetMultiplier(multiplier);
        if (fracturedWallModel.childCount > 0)
        {
            foreach (Transform child in fracturedWallModel)
            {
                var childRB = child.gameObject.AddComponent<Rigidbody>();
                child.gameObject.AddComponent<BoxCollider>();
                childRB.AddExplosionForce(20f, fracturedWallModel.position, 10f, 1f);
                Destroy(child.gameObject, .5f);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        text.SetText($"x{multiplier}");
    }
#endif
}
