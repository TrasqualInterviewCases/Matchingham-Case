using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponHandler : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] Weapon[] weapons;
    [SerializeField] Weapon activeWeapon;

    [Header("WeaponPositioning")]
    [SerializeField] Transform weaponParent;
    [SerializeField] Transform model;
    [SerializeField] Transform rightHand;
    [SerializeField] Vector3 riggedPos;
    [SerializeField] Vector3 animatedPos;
    [SerializeField] Quaternion animatedRot;
    [SerializeField] float weaponPositioningSpeed = 10f;

    [Header("IK")]
    [SerializeField] Transform rightHandTarget;
    [SerializeField] Transform leftHandTarget;
    [SerializeField] TwoBoneIKConstraint RightHandIK;
    [SerializeField] Rig rigLayer;

    [Header("Detector")]
    [SerializeField] ObstacleDetector obstacleDetector;

    private void Start()
    {
        animatedRot = weaponParent.transform.localRotation;
        ChangeWeapon(0);
    }

    private void Update()
    {
        if (obstacleDetector.DetectedTarget())
        {
            SetShootingPosition();
        }
        else
        {
            SetRunningPosition();
        }
        AlignIK();
    }

    private void SetShootingPosition()
    {
        if (RightHandIK.weight != 1)
        {
            RightHandIK.weight = 1;
            weaponParent.SetParent(model);
        }
        if (Vector3.Distance(weaponParent.transform.localPosition, riggedPos) >= 0.005f)
        {
            weaponParent.transform.localRotation = Quaternion.Lerp(weaponParent.transform.localRotation, Quaternion.identity, Time.deltaTime * weaponPositioningSpeed);
            weaponParent.transform.localPosition = Vector3.Lerp(weaponParent.transform.localPosition, riggedPos, Time.deltaTime * weaponPositioningSpeed);
        }
    }

    private void SetRunningPosition()
    {
        if (RightHandIK.weight != 0)
        {
            RightHandIK.weight = 0;
            weaponParent.SetParent(rightHand);
        }
        if (Vector3.Distance(weaponParent.transform.localPosition, animatedPos) >= 0.005f)
        {
            weaponParent.transform.localRotation = Quaternion.Lerp(weaponParent.transform.localRotation, animatedRot, Time.deltaTime * weaponPositioningSpeed);
            weaponParent.transform.localPosition = Vector3.Lerp(weaponParent.transform.localPosition, animatedPos, Time.deltaTime * weaponPositioningSpeed);
        }
    }

    public void ChangeWeapon(int weaponNo)
    {
        foreach (var weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }

        activeWeapon = weapons[weaponNo];
        activeWeapon.gameObject.SetActive(true);

        AlignIK();
    }

    private void AlignIK()
    {
        Transform rightTarget;
        Transform leftTarget;
        activeWeapon.GetIKTargets(out leftTarget, out rightTarget);
        leftHandTarget.position = leftTarget.position;
        rightHandTarget.position = rightTarget.position;
    }

    private void CloseIK()
    {
        rigLayer.weight = 0;
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerFailed += CloseIK;
        PlayerController.OnPlayerFinished += CloseIK;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerFailed -= CloseIK;
        PlayerController.OnPlayerFinished -= CloseIK;
    }
}
