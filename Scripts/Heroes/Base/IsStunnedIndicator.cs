using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsStunnedIndicator : MonoBehaviour
{
    [Header("Activate when parent gets stunned")]
    private HeroMovement targetScript;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        targetScript = transform.parent.GetComponent<HeroMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (targetScript.isStunned)
        {
            anim.SetBool("stunned", true);
        }
        else
        {
            anim.SetBool("stunned", false);
        }
    }
}
