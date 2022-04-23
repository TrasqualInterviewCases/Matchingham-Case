using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] MoveBase moveBase;

    private void Start()
    {
        ToggleShooting(true);
    }

    private void Update()
    {
        if(moveBase.CanMove())
        PlayMoveAnim();        
    }

    private void PlayMoveAnim()
    {
        var velocityX = moveBase.GetDirection().x * moveBase.HorizontaSpeed;
        var velocityZ = 1;
        anim.SetFloat("moveHorizontal", velocityX, 0.1f, Time.deltaTime);
        anim.SetFloat("moveForward", velocityZ, 0.1f, Time.deltaTime);
    }

    private void ToggleShooting(bool isShooting)
    {
        anim.SetBool("shooting", isShooting);
    }
}
