using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("ShootingSettings")]
    [SerializeField] private Transform projectilePrefab;
    [SerializeField] private Transform firepoint;

    [Header("Aim")]
    [SerializeField] private Aim aim;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shooting();
        }
    }

    private void Shooting()
    {   Vector3 aimDir = (aim.mouseWorldPosition - firepoint.position).normalized;
        Instantiate(projectilePrefab, firepoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }
}
