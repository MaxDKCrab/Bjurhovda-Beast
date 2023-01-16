using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    private PetteMove motor;

    [SerializeField] private PlayerSettings settings;

    private void Start()
    {
        motor = GetComponent<PetteMove>();
        cam = GetComponentInChildren<Camera>();
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        
        
        if (!motor.rolling)
        {
            xRotation -= (mouseY * Time.deltaTime) * settings.ySensitivity;
            xRotation = Mathf.Clamp(xRotation,-80f,80f);
            cam.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        }


        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * settings.xSensitivity);
    }
}