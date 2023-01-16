using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArm : MonoBehaviour
{
    private Animator anim;
    private bool framTagen;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PullUp()
    {
        framTagen = !framTagen;
        anim.SetBool("Framtagen",framTagen);
    }
}
