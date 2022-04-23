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

        if (detector.DetectedTarget())
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
}