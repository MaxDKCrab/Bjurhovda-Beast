using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerSettingsBox", menuName = "PlayerSettingsBox")]
public class PlayerSettings : ScriptableObject
{
    public float xSensitivity;
    public float ySensitivity;
}

