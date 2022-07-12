using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botinok : HeroMovement
{
    public ParticleSystem particle;
    public int damage=1;
    private float forcePower=12f;
    public AudioClip skillSound;
    void Awake()
    {
        heroId = 1;
        maxHeatpoints = 4;
        heatpoints = maxHeatpoints;
        skillCountdown = 3;
        
    }
    override public void Skill()
    {
        float dashForce = 13f;
        _rb.velocity = (Vector2.right * dashForce * xDirection);
        animator.SetTrigger("dash");
        audioSource.PlayOneShot(skillSound);
        particle.Play();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Hero")){
            if (!isDashing){
                //DontAttack(other.gameObject);
            }else{
                Attack(other.gameObject);
            }
        }
    }
    void DontAttack(GameObject hero){
        Rigidbody2D colRb = hero.GetComponent<Rigidbody2D>();
        HeroMovement heroScript = hero.GetComponent<HeroMovement>();
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 4f, ForceMode2D.Impulse);
        ChangeDirection();
        transform.Translate(Vector3.right * 0.01f + Vector3.up * 0.01f);
    }
    void Attack(GameObject hero){

        Rigidbody2D colRb = hero.GetComponent<Rigidbody2D>();
        Vector2 force = (hero.transform.position - (hero.transform.position + Vector3.down)).normalized * forcePower;
        //colRb.AddForce(force, ForceMode2D.Impulse);
        HeroMovement heroScript = hero.GetComponent<HeroMovement>();
        heroScript.ChangeDirection();
        heroScript.GetDamage(damage);
    }

    public bool isDashing{
        get{
            if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "botinok_dash") return true;
            return false;
        }
    }
    public override void ChangeDirection()
    {
        base.ChangeDirection();
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "botinok_dash"){
            animator.SetTrigger("startRun");
        }
    }
}
