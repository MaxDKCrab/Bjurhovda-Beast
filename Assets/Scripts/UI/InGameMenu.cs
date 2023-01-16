using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject menu;

    public void OpenClose()
    {
        menu.SetActive(!menu.activeSelf);
        if (menu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }
    
    
    
}
