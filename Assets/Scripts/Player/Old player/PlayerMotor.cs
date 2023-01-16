using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private PlayerStats stats;

    private Animator anim;
    private Camera cam;
    
    private Vector3 playerVelocity;
    private float currentSpeed;
    private bool isGrounded;
    public float checkRadius = 0.4f;
    public LayerMask groundMask;
    private Vector3 previousMovement;
    private Vector3 moveDirection;
    private Vector3 airMoveDirection;
   
    private bool crouching;
    private float crouchTimer;
    private bool lerpCrouch;
    RaycastHit hit;
    [HideInInspector] public bool rolling;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        cam = GetComponentInChildren<Camera>();

    }
    
    void Update()
    {
        isGrounded = Physics.SphereCast(controller.bounds.center,checkRadius,Vector3.down,out hit,controller.height/2 +0.2f, groundMask);

        if (crouching) currentSpeed = stats.crouchSpeed;
        else if (rolling)
        {
            currentSpeed = stats.rollSpeed;
            cam.transform.Rotate(new Vector3(1,0,0) ,stats.camRollSpeed);
        } 
        else currentSpeed = 3;
        
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;

            controller.height = Mathf.Lerp(controller.height, crouching ? stats.crouchDepth : 3, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    public void ProcessMove(Vector2 input)
    {
        
        
        if (isGrounded)
        {
            moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
        }
        else
        {
            moveDirection.x = input.x;
        }

        controller.Move(transform.TransformDirection(moveDirection) * currentSpeed * Time.deltaTime);
       
        controller.Move(transform.TransformDirection(airMoveDirection) * currentSpeed * Time.deltaTime);
       
        if (isGrounded && playerVelocity.y < 0)
        {
            controller.slopeLimit = 45.0f;
            playerVelocity = new Vector3(0,-2f,0);
        }
        
        playerVelocity.y += stats.gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
        
    }
    
    public void ProcessAnim(Vector2 input)
    {
        anim.speed = input.magnitude > 0.1 ? 3f : 1f;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            controller.slopeLimit = 100.0f;
           // playerVelocity.x = previousMovement.x * stats.jumpSpeed;
           // playerVelocity.z = previousMovement.z * stats.jumpSpeed;
            
            playerVelocity.y = Mathf.Sqrt(stats.jumpForce * -3f * stats.gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0f;
        lerpCrouch = true;
    }

    public void Roll()
    {
        rolling = !rolling;
    }
}
