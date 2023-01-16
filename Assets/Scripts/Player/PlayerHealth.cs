using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    private float lerpTimer;
    private float health;
    private float chipSpeed;

    public Image frontHealth;
    public Image backHealth;

    public Image overlay;
    public float duration;
    public float fadeSpeed;

    private float durationTimer;
    
    void Start()
    {
        chipSpeed = stats.MaxHealth * 0.02f;
        health = stats.MaxHealth;
    }

   
    void Update()
    {
        health = Mathf.Clamp(health, stats.MinHealth, stats.MaxHealth);
        UpdateHealthUI();

        if (overlay.color.a > 0)
        {
            if (health < stats.MaxHealth * 0.3) return;
              
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }


    public void UpdateHealthUI()
    {
        float fillF = frontHealth.fillAmount;
        float fillB = backHealth.fillAmount;
        float hFraction = health / stats.MaxHealth;
        if (fillB > hFraction)
        {
            frontHealth.fillAmount = hFraction;
            backHealth.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            backHealth.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            backHealth.color = Color.green;
            backHealth.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            frontHealth.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0;
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }

    public void RestoreHealth(float healing)
    {
        health += healing;
        lerpTimer = 0f;
    }
}
