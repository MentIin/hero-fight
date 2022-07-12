using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float power=20f;
    private Animator anim;
    public Transform direction;
    private AudioSource audioSource;
    public bool changeDirection = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = transform.parent.GetComponent<Animator>();
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Hero")){
            Push(other.gameObject);
        }
    }
    void Push(GameObject target){
        audioSource.Play();
        anim.SetTrigger("activate");
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        Vector3 force = (direction.position - transform.position).normalized * power;
        rb.AddForce(force, ForceMode2D.Impulse);
        if (changeDirection)
        {
            target.GetComponent<HeroMovement>().ChangeDirection();
        }

    }
}
