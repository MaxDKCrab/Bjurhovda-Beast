using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private AudioSource flickSound;
    private Light lampe;
    private System.Random rando;

    public float onIntensity = 10;
    void Start()
    {
        flickSound = GetComponent<AudioSource>();
         rando = new System.Random();
         lampe = GetComponent<Light>();
         StartCoroutine(Flick());
    }

    private IEnumerator Flick()
    {
        yield return new WaitForSeconds(rando.Next(1, 2));
        lampe.intensity = 0;
        flickSound.Play();
        yield return new WaitForSeconds(rando.Next(1, 2) - 0.9f);
        lampe.intensity = onIntensity;
        StartCoroutine(Flick());
    }
    
}
