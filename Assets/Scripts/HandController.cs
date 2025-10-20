using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public Transform playerCamera;  // 相机Transform

    [Header("Interaction Settings")]
    public float interactDistance = 3f;  // 可交互距离
    public LayerMask interactableLayer;  // 交互物体层

    private float xRotation = 0f;

    void Start()
    {
        // 锁定鼠标在屏幕中央
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
        HandleInteraction();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical");   // W/S

        // 相对玩家方向移动
        Vector3 move = transform.right * x + transform.forward * z;
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -55f, 35f); // 限制视角上下范围

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
            {
                // 检查被射中的物体是否有 IInteractable 接口
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
                else
                {
                    Debug.Log("Hit interactable object: " + hit.collider.name);
                }
            }
        }
    }
}
