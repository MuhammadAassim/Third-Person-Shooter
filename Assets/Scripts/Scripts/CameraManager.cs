using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private GameObject hipFireCam;
    [SerializeField] private GameObject ADSCam;


    private void Update()
    {
        SwitchCameras();
    }


    private void SwitchCameras()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            hipFireCam.SetActive(false);
            ADSCam.SetActive(true);
        }

        else
        {
            hipFireCam.SetActive(true);
            ADSCam.SetActive(false);
        }

    }
}
