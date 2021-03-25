using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    #region editor exposed variables
    [Header("Referances")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private LayerMask groundMask;


    [Header("Settings")]
    [Range(1f, 30f)]
    [SerializeField] private float speed = 12f;

    [Tooltip("Fall speed")]
    [Range(-50f, 0f)]
    [SerializeField] private float gravity = -15f;

    [Range(1f,10f)]
    [SerializeField] private float jumpHeight = 3f;

    // Size of ground check sphere
    // Increase if isGrounded is never true 
    // Decrease if isGrounded is always true 
    [Tooltip("Size of ground check sphere. Increase if isGrounded is never true. Decrease if isGrounded is always true ")]
    [Range(0.1f,2f)]
    [SerializeField] private float groundDistance = 0.5f;

    [Range(100f,1000f)]
    [SerializeField] private float mouseSensitivity = 250f;

    #endregion //End of editor exposed variables

    private Vector3 velocity;
    private float xRotation = 0f;
    private bool isGrounded;
    private Ray checkGround;


    private void Start(){
        Cursor.lockState = CursorLockMode.Locked;

        if(mainCamera == null) mainCamera = Camera.main.gameObject.transform;
    }

    void Update() {
        Movement();
        CameraControl();
    }


    private void Movement(){
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        // Reset velocity if grounded
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Only move in the X and Z directions if doing so wouldn't make you fall off
        Vector3 move = (transform.right * x + transform.forward * z) * speed * Time.deltaTime;
        Vector3 moveX = Vector3.Scale(move, Vector3.right);
        Vector3 moveZ = Vector3.Scale(move, Vector3.forward);

        if (IsAboveGround(transform.position + moveX)) {
            controller.Move(moveX);
        }
        
        if (IsAboveGround(transform.position + moveZ))         {
            controller.Move(moveZ);
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void CameraControl(){
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    bool IsAboveGround(Vector3 position) {
        return Physics.Raycast(position, transform.TransformDirection(Vector3.down), out _, Mathf.Infinity, groundMask);
    }

}
