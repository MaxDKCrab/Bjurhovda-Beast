using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPette : MonoBehaviour
{
    private PetteMove move;
    [SerializeField] private InGameMenu menu;
    private PlayerInput playerInput;
    public PlayerInput.OnGroundActions onGround;
    private PlayerLook look;
    private LeftArm armLeft;
    private Collector collector;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerInput = new PlayerInput();
        onGround = playerInput.OnGround;
        
        move = GetComponent<PetteMove>();
        look = GetComponent<PlayerLook>();
        armLeft = GetComponentInChildren<LeftArm>();
        collector = GetComponentInChildren<Collector>();
        
        onGround.FastFall.performed += ctx => move.fastFall();
        onGround.Jump.started += ctx => move.Jump();
        onGround.Sprint.performed += ctx => move.Roll();
        onGround.LeftArm.performed += ctx => armLeft.PullUp();
        onGround.Shoot.performed += ctx => collector.Shoot();
        onGround.EnterMenu.performed += ctx => menu.OpenClose();
    }

    private void FixedUpdate()
    {
        move.ProcessMove(onGround.Movement.ReadValue<Vector2>());
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
