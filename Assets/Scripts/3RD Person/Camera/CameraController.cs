using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform lookAtCamera;
    public Transform backAim;
    public Transform shoulderAim;

    public Transform basePositionCam;
    public Transform aimPositionCam;

    public GameObject objective;

    public float minDistance;
    public float cameraSpeed;
    public float cameraAimSpeed;

    bool isAiming = false;

    public Transform startPointToShoot;

    delegate void CameraMovement();
    CameraMovement actualMovemenmt = delegate { };
    CameraMovement aimPosition = delegate { };

    public LayerMask collisionAimRay;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        actualMovemenmt();
        aimPosition();
        transform.LookAt(lookAtCamera);
    }

    public void UpdateMovement(int dir)
    {
        if (!isAiming)
        {
            if (dir == 0)
                actualMovemenmt = GoForward;
            else if (dir == 1)
                actualMovemenmt = GoBackward;
            else if (dir == 2)
                actualMovemenmt = delegate { };
        }
    }

    public void Aim()
    {
        aimPosition = AimCam;
        isAiming = true;
    }

    public void CancelAim()
    {
        aimPosition = ThirdPersonCam;
        isAiming = false;
    }

    public void Shoot()
    {
        if (isAiming)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, collisionAimRay))
            {
                objective.transform.position = hit.point;

                //pentadent.SetObjective(objective.transform, hit.collider.transform);
                //pentadent.SetDir();
            }
            else
            {
                objective.transform.position = transform.position + transform.forward * 100;
                //pentadent.SetObjective(objective.transform, null);
                //pentadent.SetDir();
            }
        }
    }

    public void AimCam()
    {
        transform.position += (aimPositionCam.position - transform.position) * Time.deltaTime * cameraAimSpeed;
        lookAtCamera.position += (shoulderAim.position - lookAtCamera.position) * Time.deltaTime * cameraAimSpeed;

        if (Vector3.Distance(aimPositionCam.position, transform.position) < minDistance)
            aimPosition = delegate { };
    }

    public void ThirdPersonCam()
    {
        transform.position += (basePositionCam.position - transform.position) * Time.deltaTime * cameraAimSpeed;
        lookAtCamera.position += (backAim.position - lookAtCamera.position) * Time.deltaTime * cameraAimSpeed;

        if (Vector3.Distance(basePositionCam.position, transform.position) < minDistance)
            aimPosition = delegate { };
    }

    public void GoForward()
    {
        if (transform.localPosition.z < -1.5f)
        {
            transform.position += transform.forward * cameraSpeed * Time.deltaTime;
        }
    }

    public void GoBackward()
    {
        if (transform.localPosition.z > -7.5f)
            transform.position += transform.forward * -1 * cameraSpeed * Time.deltaTime;
    }
}
