using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerMovement player;

    [Header("CamFollow")]
    [SerializeField] private Transform camFollow;
    [SerializeField] private Transform aimPos;

    [Header("MouseSensitivity")]
    [SerializeField] private float mouseSens;
    [SerializeField] private float yAxisClamp;

    private float xAxis;
    private float yAxis;

    [SerializeField] private Vector3 mouseWorldPosition;
    private RaycastHit hitInfo;

    private bool isAiming;

    private void Update()
    {
        isAiming = Input.GetMouseButton(1); // Right Mouse Button for aiming

        MyInputs();
        PerformRaycast();
        HandleRotation();
        TargetPosition();
    }

    private void MyInputs()
    {
        xAxis += Input.GetAxis("Mouse X") * mouseSens;
        yAxis -= Input.GetAxis("Mouse Y") * mouseSens;
        yAxis = Mathf.Clamp(yAxis, -yAxisClamp, yAxisClamp);
    }

    private void PerformRaycast()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        Physics.Raycast(ray, out hitInfo, Mathf.Infinity);
    }

    private void HandleRotation()
    {
        camFollow.localRotation = Quaternion.Euler(yAxis, xAxis, 0);

        if (isAiming && hitInfo.collider != null)
        {
            player.SetRotationOnMove(false); // Stop moving rotation when aiming

            mouseWorldPosition = hitInfo.point;
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;

            Vector3 aimDir = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 20f);
        }
        else
        {
            player.SetRotationOnMove(true); // Allow normal movement rotation
        }
    }

    private void TargetPosition()
    {
        if (hitInfo.collider != null)
        {
            aimPos.position = hitInfo.point;
        }
    }
}
