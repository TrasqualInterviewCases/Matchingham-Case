using DG.Tweening;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Action OnPlayerFailed;
    public static Action OnPlayerFinished;

    [Header("Finish Params")]
    [SerializeField] WeaponHandler weaponHandler;
    [SerializeField] WeaponBazooka finishBazooka;
    [SerializeField] float finishMoveSpeed = 5f;
    Transform cam;

    bool shouldMoveForFinish;
    private bool isFailed;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void Update()
    {
        if (shouldMoveForFinish)
            transform.position += transform.forward * finishMoveSpeed * Time.deltaTime;
    }

    public void Fail()
    {
        if (isFailed) return;
        isFailed = true;
        OnPlayerFailed?.Invoke();
        GetComponentInChildren<Animator>().applyRootMotion = true;
        DOTween.Sequence().SetDelay(2f).OnComplete(() => GameManager.Instance.LoseGame());
    }

    public void Finish()
    {
        OnPlayerFinished?.Invoke();
        cam.SetParent(transform);
        var camPos = cam.localPosition;
        camPos.x = 0;
        cam.localPosition = camPos;
        transform.SetParent(null);
        shouldMoveForFinish = true;
        weaponHandler.ChangeWeapon(3);
    }

    public void WinGame()
    {
        GameManager.Instance.WinGame();
    }

    private void StopFinishMove()
    {
        shouldMoveForFinish = false;
        DOVirtual.DelayedCall(1f, () => WinGame());
    }

    private void OnEnable()
    {
        finishBazooka.OnOutOfAmmo += StopFinishMove;
    }

    private void OnDisable()
    {
        finishBazooka.OnOutOfAmmo -= StopFinishMove;
    }
}
