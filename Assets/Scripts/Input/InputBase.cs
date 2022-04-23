using System;
using UnityEngine;

public class InputBase : MonoBehaviour
{
    public Action OnPressed;
    public Action<Vector2> OnDrag;
    public Action OnReleased;
}
