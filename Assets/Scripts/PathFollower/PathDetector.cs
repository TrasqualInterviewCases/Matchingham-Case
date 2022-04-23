using PathCreation;
using UnityEngine;

public class PathDetector : MonoBehaviour
{
    [SerializeField] PathFollower pathFollower;

    private void Awake()
    {
        if(pathFollower == null)
        {
            pathFollower = GetComponent<PathFollower>();
        }
    }

    private void Update()
    {
        if(Physics.Raycast(transform.position + transform.forward + transform.up, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            var path = hit.collider.GetComponentInParent<PathCreator>();

            if(path != null)
            {
                pathFollower.UpdatePath(path);
            }
        }
    }
}
