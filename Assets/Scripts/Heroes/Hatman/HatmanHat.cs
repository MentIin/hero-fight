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
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        Vector2 force = transform.right * (speed * 0.7f) + (transform.up * 0.3f);
        rb.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(Die(lifetime));
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.CompareTag("Hero"))
        {
            Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), col);
            HeroMovement script = col.gameObject.GetComponent<HeroMovement>();
            if (script.red != red)
            {
                script.GetDamage(1);
                Die();
            }
        }
        else
        {
            
        }
        
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
