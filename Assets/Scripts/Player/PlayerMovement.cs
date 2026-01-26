using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //events
    public event EventHandler<OnPlayerMoveEventArgs> OnPlayerMove;
    public class OnPlayerMoveEventArgs : EventArgs
    {
        public Vector3 velocity;
        public bool isGrounded;
    }

    public event EventHandler<OnCrouchEventArgs> OnCrouchToggled;

    public class OnCrouchEventArgs : EventArgs
    {
        public bool isCrouching;
    }

    


    [SerializeField] private CharacterController controller;

    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [Header("Crouch Settings")]
    //[SerializeField] private float crouchHeight = 1f;
    //[SerializeField] private float standingHeight = 2f;
    private bool isCrouching;


    private Vector3 velocity;
    private bool isGrounded;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        Vector3 horizontalVelocity = move * speed;
        Vector3 totalVelocity = new Vector3(horizontalVelocity.x, velocity.y, horizontalVelocity.z);

        // Handle Crouch Toggle
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCrouch();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StopCrouch();
        }


        OnPlayerMove?.Invoke(this, new OnPlayerMoveEventArgs
        {
            velocity = totalVelocity,
            isGrounded = isGrounded
        });

    }

    private void StartCrouch()
    {
        if (isCrouching) return;

        isCrouching = true;
        //controller.height = crouchHeight;
        //controller.center = Vector3.up * crouchHeight / 2f;
        speed /= 2f;

        OnCrouchToggled?.Invoke(this, new OnCrouchEventArgs
        {
            isCrouching = true
        });
    }

    private void StopCrouch()
    {
        if (!isCrouching) return;

        isCrouching = false;
        //controller.height = standingHeight;
        //controller.center = Vector3.up * standingHeight / 2f;
        speed *= 2f;

        OnCrouchToggled?.Invoke(this, new OnCrouchEventArgs
        {
            isCrouching = false
        });
    }

}