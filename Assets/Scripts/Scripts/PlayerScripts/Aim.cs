using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerMovement player; // Player Movement Ko Set Karo

    [Header("CamFollow")]
    [SerializeField] private Transform camFollow; // Camera Ko Follow Karne Ki Position Set Karo
    [SerializeField] private Transform aimPos; // Aim Ki Position Set Karo

    [Header("MouseSensitivity")]
    [SerializeField] private float mouseSens; // Mouse Ki Sensitivity Set Karo
    [SerializeField] private float yAxisClamp; // Y Axis Ko Clamp Karne Ki Limit Set Karo

    [Header("Raycast Settings")]
    [SerializeField] private LayerMask raycastMask; // Layer Mask Set Karo Jisse Gun Ya UI Ignore Ho

    private float xAxis; // X Axis Ki Rotation Ki Value
    private float yAxis; // Y Axis Ki Rotation Ki Value

    [HideInInspector] public Vector3 mouseWorldPosition; // Mouse Ki World Position 
    private RaycastHit hitInfo; // Raycast Hit Information

    public bool isAiming = false; // Aiming Kar Raha Hai Ya Nahi


    private void Update()
    {
        isAiming = Input.GetMouseButton(1); // Right Mouse Button Se Aiming Ko Control Karna

        MyInputs(); // Inputs Ko Handle Karna
        PerformRaycast(); // Raycast Ko Perform Karna
        HandleRotation(); // Rotation Ko Handle Karna
        TargetPosition(); // Target Ki Position Ko Set Karna
    }

    private void MyInputs() // Mouse Inputs Ko Handle Karne Wala Function
    {
        xAxis += Input.GetAxis("Mouse X") * mouseSens; // Mouse Ki Horizontal Movement Ko Track Karna
        yAxis -= Input.GetAxis("Mouse Y") * mouseSens; // Mouse Ki Vertical Movement Ko Track Karna
        yAxis = Mathf.Clamp(yAxis, -yAxisClamp, yAxisClamp); // Y Axis Ki Rotation Ko Clamp Karna
    }

    private void PerformRaycast() // Raycast Ko Perform Karne Wala Function
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2); // Screen Ka Center
        Ray ray = Camera.main.ScreenPointToRay(screenCenter); // Screen Ke Center Se Raycast Ko Fire Karna
        Physics.Raycast(ray, out hitInfo, Mathf.Infinity, raycastMask); // Raycast Ko Perform Karna Aur Mask Use Karna
    }

    private void HandleRotation() // Player Ki Rotation Ko Handle Karne Wala Function
    {
        camFollow.localRotation = Quaternion.Euler(yAxis, xAxis, 0); // Camera Ki Rotation Ko Update Karna Based On Mouse Movement

        if (isAiming && hitInfo.collider != null) // Agar Aiming Ho Aur Hit Info Available Ho
        {
            player.SetRotationOnMove(false); // Aiming Ke Dauran Player Ki Movement Rotation Ko Disable Karna

            mouseWorldPosition = hitInfo.point; // Mouse Ki World Position Ko Set Karna
            Vector3 worldAimTarget = mouseWorldPosition; // World Aim Target Ko Set Karna
            worldAimTarget.y = transform.position.y; // Target Ki Y Position Ko Current Position Ke Barabar Set Karna

            Vector3 aimDir = (worldAimTarget - transform.position).normalized; // Aim Direction Ko Normalize Karna
            transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 20f); // Smoothly Target Ki Taraf Rotation Karna
        }
        else
        {
            player.SetRotationOnMove(true); // Agar Aiming Nahin Hai, Toh Player Ki Normal Rotation Ko Allow Karna
        }
    }

    public void TargetPosition() // Target Ki Position Ko Update Karne Wala Function
    {
        if (hitInfo.collider != null) // Agar Raycast Ne Kisi Object Ko Hit Kiya Ho
        {
            aimPos.position = hitInfo.point; // Aim Ki Position Ko Hit Point Pe Set Karna
        }
    }

    public Vector3 GetAimPoint() // Mouse Ki Hit Point Ko Dusre Scripts Se Access Karne Ke Liye
    {
        return mouseWorldPosition;
    }
}
