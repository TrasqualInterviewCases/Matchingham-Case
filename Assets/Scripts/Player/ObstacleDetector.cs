using UnityEngine;

public class ObstacleDetector : MonoBehaviour
{
    [SerializeField] LayerMask detectableMask;
    [SerializeField] float detectionDistance = 10f;

    bool canDetect;

    public bool DetectedTarget(out Transform target)
    {
        if (canDetect)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, detectionDistance, detectableMask))
            {
                target = hit.transform;
                return true;
            }
            else
            {
                target = null;
                return false;
            }
        }
        else
        {
            target = null;
            return false;
        }
    }

    private void TurnOnDetection()
    {
        canDetect = true;
    }

    private void TurnOffDetection()
    {
        canDetect = false;
    }

    private void OnEnable()
    {
        GameManager.OnGameStarted += TurnOnDetection;
        GameManager.OnGameWon += TurnOffDetection;
        GameManager.OnGameFailed += TurnOffDetection;

        PlayerController.OnPlayerFailed += TurnOffDetection;
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= TurnOnDetection;
        GameManager.OnGameWon -= TurnOffDetection;
        GameManager.OnGameFailed -= TurnOffDetection;

        PlayerController.OnPlayerFailed -= TurnOffDetection;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * detectionDistance);
    }
}
