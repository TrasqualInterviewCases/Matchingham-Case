using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] MoveBase moveBase;
    [SerializeField] ObstacleDetector detector;

    private void Update()
    {
        if(moveBase.CanMove())
        PlayMoveAnim();

        if (detector.DetectedTarget(out Transform target))
            ToggleShooting(true);
        else
            ToggleShooting(false);
    }

    private void PlayMoveAnim()
    {
        var velocityX = moveBase.GetDirection().x * moveBase.HorizontaSpeed;
        var velocityZ = 1;
        anim.SetFloat("moveHorizontal", velocityX, 0.1f, Time.deltaTime);
        anim.SetFloat("moveForward", velocityZ, 0.1f, Time.deltaTime);
    }

    public void ToggleShooting(bool isShooting)
    {
        anim.SetBool("shooting", isShooting);
    }

    public void PlayFallbackAnim()
    {
        anim.SetBool("fail", true);
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerFailed += PlayFallbackAnim;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerFailed -= PlayFallbackAnim;
    }
}
