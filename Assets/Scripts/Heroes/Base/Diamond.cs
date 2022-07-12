using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public GameObject hero;
    public Vector3 offset;
    private Animator anim;
    private HeroMovement heroMovement;
    void Awake()
    {
        heroMovement = hero.GetComponent<HeroMovement>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hero == null) { return; }
        if (heroMovement.skillCountdownTick <= 0){
            anim.SetBool("shine", true);
        }else{
            anim.SetBool("shine", false);
        }
        transform.position = hero.transform.position + offset;
    }
}
