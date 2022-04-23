using UnityEngine;

public abstract class MoveBase : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] InputBase input;

    [Space(10)]
    [SerializeField] protected MoveData data;
    [SerializeField] protected float horizontalLimit = 3f;
    [SerializeField] protected bool useRotation;
    [SerializeField] protected float rotationLimit = 25f;
    [SerializeField] protected bool autoForward = true;

    public float Speed => data.forwardSpeed * multiplier;
    public float RotationSpeed => data.rotationSpeed;


    protected float multiplier = 1f;
    protected Vector2 direction;

    public bool isOn;
    bool isInputPressed;

    public void ToggleMovementOn()
    {
        isOn = true;
    }

    public void ToggleMovementOff()
    {
        isOn = false;
    }

    public bool CanMove()
    {
        return isOn && (isInputPressed || autoForward);
    }

    protected bool CanRotate()
    {
        return useRotation;
    }

    public void ChangeSpeed(float newMultiplier)
    {
        multiplier = newMultiplier;
    }

    protected abstract void Move();

    protected virtual void Rotate() { }

    private void OnInputPressed() 
    {
        isInputPressed = true;
    }

    private void OnInputReleased()
    {
        isInputPressed = false;
        direction = Vector2.zero;
    }

    private void OnInputDrag(Vector2 newDir)
    {
        direction = newDir;
    }

    private void OnEnable()
    {
        input.OnPressed += OnInputPressed;
        input.OnReleased += OnInputReleased;
        input.OnDrag += OnInputDrag;
    }

    private void OnDisable()
    {
        input.OnPressed -= OnInputPressed;
        input.OnReleased -= OnInputReleased;
        input.OnDrag -= OnInputDrag;
    }
}
