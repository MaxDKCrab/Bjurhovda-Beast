using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Interactable
{
    [SerializeField] private float healAmount;
    protected override void Interact()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        PlayerManager.instance.player.GetComponent<PlayerHealth>().RestoreHealth(healAmount);
        Destroy(this);
    }
}
