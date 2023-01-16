using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseInput : MonoBehaviour
{

    public static GamePauseInput instance;
    
    private PlayerInput playerInput;
    public PlayerInput.GamePauseActions pauseActions;


    private void Awake()
    {
        playerInput = new PlayerInput();
        pauseActions = playerInput.GamePause;
        instance = this;
        enabled = false;
    }


    private void OnEnable()
    {
        pauseActions.Enable();
    }

    private void OnDisable()
    {
        pauseActions.Disable();
    }
}
