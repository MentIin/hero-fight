using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLighting : Bonus
{
    public GameObject lightingProjectileSpawn;
    private Transform target;
    
    

    protected override void ActivateBonus(GameObject hero)
        {
            base.ActivateBonus(hero);
            
            HeroMovement script = hero.GetComponent<HeroMovement>();
            if (script.red)
            {
                target = GameManager.S.blueHero.transform;
            }
            else
            {
                target = GameManager.S.redHero.transform;
            }
            

            GameObject spawnProjectile = Instantiate(lightingProjectileSpawn);
            spawnProjectile.transform.position = transform.position;
            LightingSummon spawnScript = spawnProjectile.GetComponent<LightingSummon>();
            spawnScript.red = script.red;

            //projectile.transform.LookAt(target);
            Vector3 dir = (target.position - transform.position);
            
            //projectile.transform.rotation = Quaternion.LookRotation(dir);
            //projectile.transform.Rotate(Vector3.down * 90);

            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            spawnScript.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            //spawnProjectile.transform.Rotate(Vector3.forward, 0f);

        }
}
