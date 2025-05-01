using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("ShootingSettings")]
    [SerializeField] private Transform projectilePrefab; // Projectile Ka Prefab Set Karo
    [SerializeField] private Transform firepoint; // Firepoint Ki Position Set Karo

    [Header("Aim")]
    [SerializeField] private Aim aim; // Aim Script Ko Set Karo

    private bool isAiming;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }

        // Only allow shooting when aiming
        if (Input.GetKeyDown(KeyCode.Mouse0) && isAiming)
        {
            Shooting();
        }
    }

    private void Shooting() // Shooting Karne Wala Function
    {
        // Aim Ki World Position Se Firepoint Tak Ka Direction Calculate Karna
        Vector3 aimDir = (aim.mouseWorldPosition - firepoint.position).normalized;

        Instantiate(projectilePrefab, firepoint.position, Quaternion.LookRotation(aimDir, Vector3.up)); // Projectile Ko Instantiate Karna, Firepoint Se Aim Direction Ki Taraf
    }
}
