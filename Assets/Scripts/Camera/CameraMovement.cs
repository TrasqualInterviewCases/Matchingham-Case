using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] PathMove player;
    [SerializeField] float limit = 1.5f;

    private void LateUpdate()
    {
        if (!player.CanMove()) return;

        transform.localPosition = Vector3.Lerp(new Vector3(-limit, transform.localPosition.y, transform.localPosition.z), new Vector3(limit, transform.localPosition.y, transform.localPosition.z), (player.transform.localPosition.x + 2) / 4);
    }
}
