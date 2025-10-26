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
    public LayerMask groundMask;

    private Interactable currentTarget;

    private float xRotation = 0f;
    private Rigidbody rb;
    private CharacterController controller;
    public GameObject interactText;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    Vector3 velocity;
    public float gravity = -20f;

    void Start()
    {
        // �����������Ļ����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        
        HandleLook();
       // HandleInteraction();
        if (Input.GetKeyDown(KeyCode.E) && currentTarget != null)
        {
            currentTarget.Interact();
        }
    }

     void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
   
        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical");   // W/S

        // �����ҷ����ƶ�
        Vector3 move = transform.right * x + transform.forward * z;
        //transform.position += move * moveSpeed * Time.deltaTime;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // ȷ����ɫ����
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
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



}
