using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerStatsBox", menuName = "PlayerStatsBox")]
public class PlayerStats : ScriptableObject
{
    public float MaxHealth;
    public float MinHealth;
    public float runSpeed;
    public float maxAccel;
    public float rollAccel;
    public float airDeccel;
    public float gravity;
    public float fastFallSpeed;
    public float rollSpeed;
    public float camRollSpeed;
    public float jumpForce;
    public float crouchSpeed;
    public float crouchDepth;
}
