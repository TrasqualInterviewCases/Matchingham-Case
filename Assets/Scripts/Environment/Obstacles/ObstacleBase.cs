using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out PlayerController player))
        {
            player.Fail();
        }  
    }

    protected virtual void Move() { }
}
