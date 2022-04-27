using UnityEngine;

public class speeder : MonoBehaviour
{
    [SerializeField] float speed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out MoveBase player))
        {
            player.ChangeSpeed(speed);
        }
    }

    private void OnDrawGizmos()
    {
        var col = GetComponent<BoxCollider>();
        Gizmos.color = Color.red;
        Matrix4x4 cubeT = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.matrix *= cubeT;
        Gizmos.DrawWireCube(col.center, col.size);
        Gizmos.matrix = oldMatrix;
    }
}
