using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject shieldsEf;
    private bool red;
    private float knockback = 8f;
    // Start is called before the first frame update
    void Start()
    {
        shieldsEf = transform.parent.transform.parent.gameObject;
        red = shieldsEf.GetComponent<ShieldsEffect>().red;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            if (other.GetComponent<DroneProjectile>().red != red)
            {
                Destroy(this.gameObject);
                Destroy(other.gameObject);
            }
        }else if (other.gameObject.CompareTag("Hero")){
            if (other.GetComponent<HeroMovement>().red == red) return;
            Vector2 force = (other.transform.position - transform.position).normalized * knockback;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            other.gameObject.GetComponent<HeroMovement>().ChangeDirection();
            Destroy(this.gameObject);
        }

    }
}
