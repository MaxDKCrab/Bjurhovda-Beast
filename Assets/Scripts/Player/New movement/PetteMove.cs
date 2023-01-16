using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PetteMove : MonoBehaviour
{
	Rigidbody rb;

	[SerializeField] private PlayerStats stats;
	public LayerMask groundLayer;
	public float checkRadius;
	private RaycastHit hit;
	private float crouchTimer;
	private bool lerpCrouch;
	private float currentSpeed;
	private float currentAccel;
	private bool crouching;
	private float inputMagnitude;
	private Camera cam;
	private CapsuleCollider capCollider;
	[HideInInspector] public bool rolling;

	void Start()
	{
		capCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

	public void fastFall()
	{
		rb.velocity = new Vector3(rb.velocity.x, stats.fastFallSpeed, rb.velocity.z);
		crouching = !crouching;
		crouchTimer = 0f;
		lerpCrouch = true;
	}

	private void Update()
	{
		if (rolling)
		{
			currentSpeed = stats.rollSpeed;
			currentAccel = stats.rollAccel;
			cam.transform.Rotate(new Vector3(1,0,0) ,stats.camRollSpeed * inputMagnitude);

		}
		else
		{
			currentSpeed = stats.runSpeed;
			currentAccel = stats.maxAccel;
		}
		
		
		if (lerpCrouch)
		{
			crouchTimer += Time.deltaTime;
			float p = crouchTimer / 1;
			p *= p;

			capCollider.height = Mathf.Lerp(capCollider.height, crouching ? stats.crouchDepth : 3, p);

			if (p > 1)
			{
				lerpCrouch = false;
				crouchTimer = 0f;
			}
		}
	}

	public void ProcessMove(Vector2 input)
	{

		inputMagnitude = input.magnitude;
	    
	    if(IsGrounded())
	    {
		    Vector3 inputDir = transform.forward * input.y + transform.right * input.x;
		    rb.velocity = Accelerate(inputDir);
	    }

	    if(!IsGrounded())
	    {
		    AirDecelerate(input);
	    }
    }

    bool IsGrounded()
    {
	    return Physics.SphereCast(capCollider.bounds.center,checkRadius,Vector3.down,out hit,capCollider.height /2 +0.2f, groundLayer);
    }

    public void Jump()
    {
	    if (IsGrounded())
	    {
		    rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(stats.jumpForce * -3f * stats.gravity), rb.velocity.z);
	    }
    }

    Vector3 Accelerate(Vector3 dir){
    	RaycastHit rayHit;
    	Physics.Raycast(transform.position, -transform.up, out rayHit);
    	Vector3 floorNormal = rayHit.normal;

    	float angleX = Mathf.Atan2(floorNormal.z, floorNormal.y) * Mathf.Rad2Deg;
    	float angleZ = Mathf.Atan2(floorNormal.x, floorNormal.y) * Mathf.Rad2Deg;

    	dir = Quaternion.Euler(angleX, 0f, angleZ) * dir;
    	dir = new Vector3(dir.x, -dir.y, dir.z);

		Vector3 vel = rb.velocity;
		Vector3 changeDir = dir.normalized * currentSpeed - vel;
		float changeAmount = Mathf.Clamp(changeDir.magnitude, 0, currentAccel*Time.deltaTime);
		Vector3 returnVel = vel + changeDir.normalized * changeAmount;

		return returnVel;
    }

    public void Roll()
    {
	    rolling = !rolling;
    }
    
    
    void AirDecelerate(Vector2 input){
    	Vector2 moveInput = input;
    	Vector3 localVel = transform.InverseTransformDirection(rb.velocity);

    	//If player has speed in one direction and holds opposite, negative force is applied
    	if(localVel.x >= 0 && moveInput.x < -0.2f){rb.AddForce(transform.right * stats.airDeccel * moveInput.x); }
    	else if(localVel.x <= 0 && moveInput.x > 0.2f){rb.AddForce(transform.right * stats.airDeccel  * moveInput.x); }
    	if(localVel.z >= 0 && moveInput.y < -0.2f){rb.AddForce(transform.forward * stats.airDeccel  * moveInput.y); }
    	else if(localVel.z <= 0 && moveInput.y > 0.2f){rb.AddForce(transform.forward * stats.airDeccel  * moveInput.y); }
    }
}
