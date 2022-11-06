using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class ShootGunCollector : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokeParticle;
    private Animator anim;
    private bool gunCDbool = false;
    [SerializeField] private float shotRange;
    private System.Random rando;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject hitParticle;

    void Start()
    {
        anim = GetComponent<Animator>();
        rando = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) && !gunCDbool)
        {
            StartCoroutine(Shoot());
        }
       
        
    }
    
    private IEnumerator Shoot()
    {
        gunCDbool = true;
        anim.SetTrigger("Shoot");
        smokeParticle.Play();
        RaycastHit hit;
        Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, shotRange);
        Instantiate(hitParticle,hit.point,quaternion.identity);
        yield return new WaitForSeconds(rando.Next(0,2));
        gunCDbool = false;
    }
}
