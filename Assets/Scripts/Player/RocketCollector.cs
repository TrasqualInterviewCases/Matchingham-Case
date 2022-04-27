using UnityEngine;

public class RocketCollector : MonoBehaviour
{
    [SerializeField] WeaponBazooka bazooka;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ProjectileRocket rocket))
        {
            rocket.GetCollected();
            bazooka.AddAmmo(rocket.Amount);
        }
    }
}
