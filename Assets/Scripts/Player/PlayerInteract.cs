using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float rayDistance = 3f;
    [SerializeField] private LayerMask interactMask;
    private PlayerUI playerUI;
    private InputPette input;
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        input = GetComponent<InputPette>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateCrosshair(false);
        
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin,ray.direction * rayDistance);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, rayDistance, interactMask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateCrosshair(true);
                
                if (input.onGround.Interact.triggered)
                {
                    interactable.BaseInteract();   
                }
            }
        }
    }
}
