using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image crosshair;

    public Sprite defaultCross;
    public Sprite interactCross;
    
    public void UpdateCrosshair(bool isInteract)
    {
        if (isInteract)
        {
            crosshair.sprite = interactCross;
        }
        else
        {
            crosshair.sprite = defaultCross;
        }
    }
    
}
