using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnGroundActions onGround;
    private PlayerMotor motor;
    private PlayerLook look;
    private LeftArm armLeft;
    
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked; 
    
        playerInput = new PlayerInput();
        onGround = playerInput.OnGround;
        
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        armLeft = GetComponentInChildren<LeftArm>();

        onGround.LeftArm.performed += ctx => armLeft.PullUp();
        onGround.Jump.performed += ctx => motor.Jump();
        onGround.Sprint.performed += ctx => motor.Roll();
        onGround.FastFall.performed += ctx => motor.Crouch();
    }

    private void FixedUpdate()
    {
        motor.ProcessMove(onGround.Movement.ReadValue<Vector2>());
        motor.ProcessAnim(onGround.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onGround.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onGround.Enable();
    }

    private void OnDisable()
    {
        onGround.Disable();
    }
}
