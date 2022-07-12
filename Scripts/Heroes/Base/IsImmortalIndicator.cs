using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsImmortalIndicator : MonoBehaviour
{
    [Header("Activate when parent is immortal")]
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
        if (targetScript.isImmortal && !targetScript.isStunned)
        {
            anim.SetBool("immortal", true);
        }
        else
        {
            anim.SetBool("immortal", false);
        }
    }
}
