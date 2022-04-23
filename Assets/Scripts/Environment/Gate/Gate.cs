using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] int gateItemNo;
    [SerializeField] MeshRenderer[] renderers;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out WeaponHandler weaponHandler))
        {
            weaponHandler.ChangeWeapon(gateItemNo);
            CloseGate();
        }
    }

    private void CloseGate()
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
        }
    }
}
