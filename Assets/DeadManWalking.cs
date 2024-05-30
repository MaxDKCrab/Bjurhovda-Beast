using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadManWalking : MonoBehaviour
{

    public GameObject deadManWalking;
    public AudioSource deadManWalkingNoise;

    private void Start()
    {
        deadManWalkingNoise = GetComponent<AudioSource>();  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null) 
        {
        
            deadManWalking.SetActive(true);
            deadManWalkingNoise.Play();
        
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.GetComponent<PlayerHealth>() != null)
        {

            deadManWalking.SetActive(false);
            deadManWalkingNoise.Stop();

        }

    }
}

        
