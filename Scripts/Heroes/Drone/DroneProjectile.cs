using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneProjectile : MonoBehaviour
{
    protected float speed=11f;
    public GameObject boom;
    public bool red;
    // Start is called before the first frame update
    void Start()
    {
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (transform.position.y < -20f){
            Destroy(this.gameObject);
        }

        
        RaycastHit2D hitInfo = Physics2D.CircleCast(transform.position, 0.1f, Vector2.zero);
        
        if (hitInfo.collider.CompareTag("Ground"))
        {
            Die();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hero")){
            if (other.gameObject.GetComponent<HeroMovement>().red != red)
            {
                other.GetComponent<HeroMovement>().GetDamage(1);
                Die();
            }
        } else if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Ground")){
            Die();
        }
    }

    void Die(){
        Destroy(this.gameObject);
        GameObject bm = Instantiate(boom, transform.position, Quaternion.Euler(0, 0, 0));
        bm.transform.Translate(Vector2.down * 0.05f);
        
    }
}
