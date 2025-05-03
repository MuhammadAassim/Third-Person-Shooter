using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("ShootingSettings")]
    [SerializeField] private Transform projectilePrefab; // Projectile Ka Prefab Set Karo
    [SerializeField] private Transform firepoint; // Fire Karne Ki Jagah (Firepoint) Set Karo

    [Header("Aim")]
    [SerializeField] private Aim aim; // Aim Script Ko Set Karo Jo Mouse Ki Position De Raha Hai

    private bool isAiming; // Yeh Check Karne Ke Liye Ke Player Aim Kar Raha Hai Ya Nahi

    private Animator animator; // Animator Set Karo 



    private void Start()
    {
        animator = GetComponentInParent<Animator>(); // Animator Component Liya
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1)) // Right Click Press Kiya Gaya Hai
        {
            isAiming = true; // Aim On Hai
        }
        else
        {
            isAiming = false; // Aim Off Hai
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isAiming) // Agar Left Click Aur Aim On Hai
        {
            Shooting(); // Fire Kare
            animator.SetTrigger("Shoot"); //Shooting Animation Play Hogi
        }
    }

    private void Shooting() // Fire Karne Ka Function
    {
        Vector3 aimDir = (aim.mouseWorldPosition - firepoint.position).normalized; // Aim Ki Direction Calculate Karna

        Instantiate(projectilePrefab, firepoint.position, Quaternion.LookRotation(aimDir, Vector3.up)); // Naya Projectile Firepoint Se Aim Direction Mein Bhejna
    }
}
