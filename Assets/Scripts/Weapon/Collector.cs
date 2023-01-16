using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private WeaponStats wStats;
    
    private Camera fpsCam;
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem muzzleFlash;
    private float nextTimeToFire = 0f;
    private static readonly int CollecRe = Animator.StringToHash("CollecRe");


    private void Start()
    {
        fpsCam = GetComponentInParent<Camera>();
    }

    public void Shoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / wStats.fireRate;
        
            anim.SetTrigger(CollecRe);
            muzzleFlash.Play();
            RaycastHit hit;
        
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, wStats.range))
            {
                Debug.Log(hit.transform.name);
                Damagable damagable = hit.transform.GetComponent<Damagable>();
                if (damagable != null)
                {
                    damagable.TakeDamage(wStats.damage,hit);
                }

            }
        }
    }
    
}
