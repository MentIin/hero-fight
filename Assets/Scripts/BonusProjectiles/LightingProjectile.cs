using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LightingProjectile : MonoBehaviour
{
    public GameObject lighting;
    private int lightingNum = 7;
    private BoxCollider2D collider;
    public bool red;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        SpawnLighting();
        Damage();
        Destroy(this.gameObject, 0.1f);
    }

    void SpawnLighting()
    {
        for (int i = 0; i < lightingNum; i++)
        {
            var pos = transform.position + transform.right * (21.875f / lightingNum * ( i));
            Instantiate(lighting, pos, transform.rotation);
        }
    }

    private void FixedUpdate()
    {
        Damage();
    }

    void Damage()
    {
        RaycastHit2D[] hitInfo = new RaycastHit2D[20];
        collider.Cast(Vector2.zero, hitInfo);

        foreach (var i in hitInfo)
        {
            if (!i) continue;
            if (i.collider.CompareTag("Hero"))
            {
                var script = i.collider.gameObject.GetComponent<HeroMovement>();
                if (script.red != red)
                {
                    script.GetDamage(1);
                }
            }
        }
    }
}
