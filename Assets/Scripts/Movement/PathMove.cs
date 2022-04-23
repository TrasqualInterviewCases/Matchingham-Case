using UnityEngine;

public class PathMove : MoveBase
{
    [SerializeField] Transform model;

    Vector3 rotationSmoothVel;
    Vector3 prevXDir;

    protected override void Move()
    {
        var localPos = transform.localPosition;
        var horizontal = direction.x * data.horizontalSpeed;
        localPos += new Vector3(horizontal, 0f, 0f);
        localPos.x = Mathf.Clamp(localPos.x, -horizontalLimit, horizontalLimit);
        transform.localPosition = localPos;
    }

    protected override void Rotate()
    {
        var horizontal = direction.x * data.horizontalSpeed;
        var dir = new Vector3(horizontal, model.localPosition.y, data.forwardSpeed * Time.deltaTime);
        dir = Vector3.SmoothDamp(prevXDir, dir, ref rotationSmoothVel, 0.2F);
        prevXDir = dir;

        var currentRot = model.localRotation;
        var targetRot = Quaternion.LookRotation(dir);
        model.localRotation = Quaternion.Lerp(currentRot, targetRot, Time.deltaTime * data.rotationSpeed);

        var ry = model.localEulerAngles.y;
        if (ry >= 180) ry -= 360;
        model.localEulerAngles = new Vector3(0, Mathf.Clamp(ry, -rotationLimit, rotationLimit), 0);
    }

    private void Update()
    {
        if (CanMove())
        {
            Move();

            if (CanRotate())
            {
                Rotate();
            }
        }
    }
}
