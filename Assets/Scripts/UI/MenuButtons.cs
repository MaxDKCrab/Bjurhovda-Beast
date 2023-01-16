using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{

    public PlayerSettings settings;

    [SerializeField] private Slider sliderX;
    [SerializeField] private Slider sliderY;

    private void Start()
    {
        sliderX.value = settings.xSensitivity;
        sliderY.value = settings.ySensitivity;
    }

    public void ChangeSensSliderX(float sens)
    {
        settings.xSensitivity = sens;
    }

    public void ChangeSensSliderY(float sens)
    {
        settings.ySensitivity = sens;
    }
    
    public void Gaming()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }
    
}
