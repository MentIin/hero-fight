using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusShield : Bonus
{
    public GameObject shieldsEffect;
    protected override void ActivateBonus(GameObject hero)
    {
        base.ActivateBonus(hero);
        HeroMovement script = hero.GetComponent<HeroMovement>();
        script.ImmortalityOn(5f, false);
        
        //GameObject shldEf = Instantiate(shieldsEffect, shieldsEffect.transform.position, shieldsEffect.transform.rotation);
        //shldEf.GetComponent<ShieldsEffect>().target = hero.transform;
    }
}
