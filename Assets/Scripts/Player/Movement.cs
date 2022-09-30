using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    public float speed;
    public float sprintSpeed;
    private Vector3 velocity;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;
    public bool jumpOn;
    [SerializeField] private float jumpForce;
    // [SerializeField] private Animator anim;
    public bool cantMove;

    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isGrounded && Input.GetButtonDown("Jump") && jumpOn)
        {
            velocity.y += jumpForce;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;

        if (!cantMove)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                controller.Move(move * sprintSpeed * Time.deltaTime);
                // if (move.magnitude > 0.1f)
                // {
                //     anim.speed = 1.75f;
                // }
            }
            else
            {
                controller.Move(move * speed * Time.deltaTime);
                // anim.speed = 1f;
            }
        }
        

        // if (move.magnitude > 0.1f)
        // {
        //     anim.SetBool("IsMoving", true);
        // }
        // else
        // {
        //     anim.SetBool("IsMoving", false);
        // }

        
        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
