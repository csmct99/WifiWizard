using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public float speed = 12f;
    public float gravity = -15f;
    public float jumpHeight = 3f;

    // Size of ground check sphere
    // Increase if isGrounded is never true 
    // Decrease if isGrounded is always true 
    public float groundDistance = 0.5f;

    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    Ray checkGround;


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        // Reset velocity if grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // undo move if not above anything
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out _))
        {
            Vector3 undo = transform.right * x * -1 + transform.forward * z * -1;
            controller.Move(undo * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
