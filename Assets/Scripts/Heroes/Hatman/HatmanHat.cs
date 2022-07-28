using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatmanHat : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D rb;

    private float timeFromSpawn=0f;

    public bool red;

    private float speed = 4f;

    private float lifetime = 5f;
    private Collider2D col;
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();

        Vector2 force = transform.right * (speed * 0.8f) + (transform.up * 0.2f);
        rb.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(Die(lifetime));

        rb.gravityScale = 0.05f;


    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.CompareTag("Hero"))
        {
            Physics2D.IgnoreCollision(col, collider);
            HeroMovement script = collider.gameObject.GetComponent<HeroMovement>();
            if (script.red != red && !script.isImmortal)
            {
                script.GetDamage(1);
                Die();
            }
        }
        else
        {
            
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        rb.gravityScale = 0.4f;
    }


    void Die()
    {
        rb.velocity = rb.velocity * 0.7f;
        _anim.SetBool("shading", true);

        Destroy(this.gameObject, 1f);
    }

    IEnumerator Die(float time)
    {
        yield return new WaitForSeconds(time);
        Die();
    }
}
