using PathCreation;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    float distance;
    PathCreator path;
    MoveBase moveBase;
    Quaternion prevRot;

    private void Awake()
    {
        moveBase = GetComponentInChildren<MoveBase>();
    }

    private void Update()
    {
        if (!moveBase.CanMove()) return;
        UpdateDistance();

        var currentPos = transform.position;
        var targetPos = path.path.GetPointAtDistance(distance, EndOfPathInstruction.Stop);
        transform.position = Vector3.Lerp(currentPos, targetPos, Time.deltaTime * moveBase.Speed);
        transform.rotation = Quaternion.Lerp(prevRot, path.path.GetRotationAtDistance(distance, EndOfPathInstruction.Stop), Time.deltaTime * moveBase.RotationSpeed);
        prevRot = transform.rotation;
    }

    void UpdateDistance()
    {
        if (path != null)
        {
            distance = path.path.GetClosestDistanceAlongPath(transform.position + transform.forward * (Time.deltaTime * moveBase.Speed));
        }
    }

    void ResetDistance()
    {
        distance = path.path.GetClosestDistanceAlongPath(transform.position);
    }

    public void UpdatePath(PathCreator newPath)
    {
        if (path != newPath)
        {
            path = newPath;
            ResetDistance();
        }
    }
}
