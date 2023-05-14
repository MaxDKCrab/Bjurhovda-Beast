using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public float enemyMaxHP = 50;
    private float enemyHealth;
    [SerializeField] private GameObject deathPrefab;
    [SerializeField] private ParticleSystem hitParticle;
    
    private void Start()
    {
        enemyHealth = enemyMaxHP;
    }

    public void TakeDamage(float amount, RaycastHit hit)
    {
        Instantiate(hitParticle, hit.point, Quaternion.LookRotation(hit.normal));
        enemyHealth -= amount;
        if (enemyHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        if (deathPrefab != null)
        {
            Instantiate(deathPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
    

    private void Update()
    {
        enemyHealth = Mathf.Clamp(enemyHealth, 0f, enemyMaxHP);
    }
}
