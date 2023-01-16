using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon stat block", menuName = "Weapon stat block")]
public class WeaponStats : ScriptableObject
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
}
