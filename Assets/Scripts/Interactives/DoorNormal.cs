using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNormal : Interactable
{
    public bool doorOpen;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Interact()
    {

        animator.SetTrigger("Öppna");
        
    }
}
