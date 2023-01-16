using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float destroyTimer = 2f;
    private void Start()
    {
        Destroy(gameObject, destroyTimer);
    }
}
