using UnityEngine;

public class SwerveInput : InputBase
{
    float horizontal;
    float prevHorizontal;
    float horDelta;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            prevHorizontal = Input.mousePosition.x;

            OnPressed?.Invoke();
        }

        else if (Input.GetMouseButton(0))
        {
            horDelta = Input.mousePosition.x - prevHorizontal;

            prevHorizontal = Input.mousePosition.x;

            OnDrag?.Invoke(new Vector2(CalculateSmoothedHorizontalValue(), 1));
        }

        else if (Input.GetMouseButtonUp(0))
        {
            horDelta = 0f;

            CalculateSmoothedHorizontalValue();

            OnReleased?.Invoke();
        }
    }

    private float CalculateSmoothedHorizontalValue()
    {
        var current = horizontal;

        var target = (Mathf.Lerp(0, 1, Mathf.Abs(horDelta) / Screen.width)) * Mathf.Sign(horDelta);

        horizontal = Vector2.MoveTowards(new Vector2(current, 0), new Vector2(target, 0), Time.deltaTime).x;

        return horizontal;
    }
}
