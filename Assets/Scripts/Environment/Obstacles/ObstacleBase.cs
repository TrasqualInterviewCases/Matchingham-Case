using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //killPlayer    
    }

    protected virtual void Move() { }
}
