using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{


    public void Gaming()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    
    
}
