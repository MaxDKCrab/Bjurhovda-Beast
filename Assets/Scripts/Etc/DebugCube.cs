using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCube : MonoBehaviour
{
    public bool drawOnSelect;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (!drawOnSelect)
        {
            Gizmos.DrawWireCube(transform.position,transform.localScale);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (drawOnSelect)
        {
            Gizmos.DrawWireCube(transform.position,transform.localScale);
        }
    }
}
