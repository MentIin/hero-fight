using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BonusLighting : Bonus
{
    public GameObject lightingProjectileSpawn;
    private Vector3 target;
    
    

    protected override void ActivateBonus(GameObject hero)
        {
            base.ActivateBonus(hero);
            
            
            HeroMovement script = hero.GetComponent<HeroMovement>();
            if (PlayerPrefs.GetInt("mode") == 0)
            {
                if (script.red){
                    target = GameManager.S.blueHero.transform.position;
                }
                else {
                    target = GameManager.S.redHero.transform.position;
                }
            }
            else
            {
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemies.Length == 0)
                {
                    target = script.transform.position + UnityEngine.Random.insideUnitSphere;
                }
                else
                {
                    print("qwer");
                }
            }
            
            

            GameObject spawnProjectile = Instantiate(lightingProjectileSpawn);
            spawnProjectile.transform.position = transform.position;
            LightingSummon spawnScript = spawnProjectile.GetComponent<LightingSummon>();
            spawnScript.red = script.red;
            
            
            
            Vector3 dir = (target - transform.position);
            
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            spawnScript.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            

        }
}
