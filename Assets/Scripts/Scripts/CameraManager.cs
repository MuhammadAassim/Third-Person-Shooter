using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private GameObject hipFireCam; // Hip Fire Camera Set Karna
    [SerializeField] private GameObject ADSCam; // Aim Down Sights Camera Set Karna


    private void Update()
    {
        SwitchCameras(); // Camera Ko Switch Karne Wala Function Call Karna
    }

    private void SwitchCameras() // Camera Switch Karne Wala Function
    {
        // Agar Right Mouse Button Dabaya Hai Toh ADS Camera Show Karna
        if (Input.GetKey(KeyCode.Mouse1))
        {
            hipFireCam.SetActive(false); // Hip Fire Camera Ko Band Karna
            ADSCam.SetActive(true); // ADS Camera Ko On Karna
        }
        else
        {
            hipFireCam.SetActive(true); // Hip Fire Camera Ko On Karna
            ADSCam.SetActive(false); // ADS Camera Ko Band Karna
        }
    }
}
