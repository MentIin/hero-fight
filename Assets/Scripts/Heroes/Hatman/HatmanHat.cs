﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatmanHat : MonoBehaviour
{
    private Rigidbody2D rb;

    private float timeFromSpawn=0f;

    public bool red;

    private float speed=6;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 force = transform.right * speed;
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Hero"))
        {
            HeroMovement script = collision.collider.gameObject.GetComponent<HeroMovement>();
            if (script.red != red)
            {
                script.GetDamage(1);
                Die();
            }
        }
        else
        {
            Die();
        }
        
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}