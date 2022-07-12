using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSkillCooldown : Bonus
{
    protected override void ActivateBonus(GameObject hero)
    {
        hero.GetComponent<HeroMovement>().SpeedUpSkillCooldown(8f);
        Destroy(this.gameObject);
    }
}
