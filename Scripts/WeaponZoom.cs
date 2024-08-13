using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float zoomedInFOV = 30f;
    [SerializeField] float zoomedInSensitivity = 0.5f;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedOutSensitivity = 2f;
    [SerializeField] FPSController FPSControl;

    bool isZoomedIn = false;
    // Start is called before the first frame update
    private void OnEnable()
    {
        ZoomOut(); isZoomedIn = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(isZoomedIn)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            isZoomedIn = !isZoomedIn;
        }
    }

    private void ZoomIn()
    {
        cam.fieldOfView = zoomedInFOV;
        FPSControl.lookSpeed = zoomedInSensitivity;
    }

    private void ZoomOut()
    {
        cam.fieldOfView = zoomedOutFOV;
        FPSControl.lookSpeed = zoomedOutSensitivity;
    }
    private void OnDisable()
    {
        ZoomOut();
        isZoomedIn = false;
    }
}
