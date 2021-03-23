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

    bool AboveGround(Vector3 position)
    {
        return Physics.Raycast(position, transform.TransformDirection(Vector3.down), out _);
    }

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

        // Only move in the X and Z directions if doing so wouldn't make you fall off
        Vector3 move = (transform.right * x + transform.forward * z) * speed * Time.deltaTime;
        Vector3 moveX = Vector3.Scale(move, Vector3.right);
        Vector3 moveZ = Vector3.Scale(move, Vector3.forward);

        if (AboveGround(transform.position + moveX))
        {
            controller.Move(moveX);
        }
        if (AboveGround(transform.position + moveZ))
        {
            controller.Move(moveZ);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
