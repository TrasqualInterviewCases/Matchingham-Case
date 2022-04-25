using UnityEngine;

public class ObstacleDetector : MonoBehaviour
{
    [SerializeField] LayerMask detectableMask;
    [SerializeField] float detectionDistance = 10f;

    bool canDetect = true;

    public bool DetectedTarget()
    {
        if (canDetect)
        {
            if (Physics.Raycast(transform.position, transform.forward, detectionDistance, detectableMask))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
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
        PlayerController.OnPlayerFinished += TurnOffDetection;
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= TurnOnDetection;
        GameManager.OnGameWon -= TurnOffDetection;
        GameManager.OnGameFailed -= TurnOffDetection;

        PlayerController.OnPlayerFailed -= TurnOffDetection;
        PlayerController.OnPlayerFinished -= TurnOffDetection;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * detectionDistance);
    }
}
