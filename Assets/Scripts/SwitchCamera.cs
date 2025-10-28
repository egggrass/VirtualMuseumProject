using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour, Interactable
{
    public Camera camera1;
    public Camera camera2;
    private bool usingCam1 = true;

    void Start()
    {
        camera1.enabled = true;
        camera2.enabled = false;
    }
    public void Interact()
    {
        usingCam1 = !usingCam1;
        camera1.enabled = usingCam1;
        camera2.enabled = !usingCam1;
    }

    void OnTriggerEnter(Collider other)
    {
       
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            camera1.enabled = true;
            camera2.enabled = false;
        }
    }

}
