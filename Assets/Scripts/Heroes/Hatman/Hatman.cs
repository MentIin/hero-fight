using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatman : HeroMovement
{
    [SerializeField] private GameObject hatmanHat;
    [SerializeField] private Transform hatmanHatSpawnPoint;
    
    public override void Jump()
    {
        base.Jump();
        
    }

    public override void Skill()
    {
        animator.SetTrigger("attack");
        StartCoroutine(SpawnHat());
    }

    IEnumerator SpawnHat()
    {
        yield return new WaitForSeconds(0.835f);
        GameObject hat = Instantiate(hatmanHat, hatmanHatSpawnPoint.position, transform.rotation);
        HatmanHat script = hat.GetComponent<HatmanHat>();
        script.red = red;
        Physics2D.IgnoreCollision(hat.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}
