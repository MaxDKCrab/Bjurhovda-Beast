using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;
    private GamePauseInput pauseInput;

    private void Start()
    {
        pauseInput = GamePauseInput.instance;
    }

    public void GamePause(bool paused)
    {
        if (paused)
        {
            player.GetComponent<InputPette>().enabled = false;
            pauseInput.enabled = true;
        }
        else
        {
            player.GetComponent<InputPette>().enabled = true;
            pauseInput.enabled = false;
        }
        
    }

}
