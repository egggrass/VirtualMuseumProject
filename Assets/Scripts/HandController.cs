using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public Transform playerCamera;  // ���Transform

    [Header("Interaction Settings")]
    public float interactDistance = 3f;  // �ɽ�������
    public LayerMask interactableLayer;  // ���������

    private Interactable currentTarget;

    private float xRotation = 0f;
    public GameObject interactText;

    void Start()
    {
        // �����������Ļ����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
       // HandleInteraction();
        if (Input.GetKeyDown(KeyCode.E) && currentTarget != null)
        {
            currentTarget.Interact();
        }
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical");   // W/S

        // �����ҷ����ƶ�
        Vector3 move = transform.right * x + transform.forward * z;
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -55f, 35f); // �����ӽ����·�Χ

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            currentTarget = interactable;
            Debug.Log("Player entered interaction zone: " + other.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<Interactable>();
        if (interactable == currentTarget)
        {
            currentTarget = null;
            Debug.Log("Player left interaction zone");
        }
    }



    /* void HandleInteraction()
     {
         if (Input.GetKeyDown(KeyCode.E))
         {

             Ray ray = new Ray(playerCamera.position, playerCamera.forward);
             RaycastHit hit;

             Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red, 1f);

             if (Physics.Raycast(ray, out hit, interactDistance , interactableLayer))
             {
                 // ��鱻���е������Ƿ��� Interactable �ӿ�
                 Interactable interactable = hit.collider.GetComponent<Interactable>();
                 if (interactable != null)
                 {
                     interactText.SetActive(true);
                     interactable.Interact();
                     Debug.Log("Interact");
                 }
                 else
                 {
                     interactText.SetActive(false);
                     Debug.Log("Hit interactable object: " + hit.collider.name);
                 }
             }
         }
     }*/
}
