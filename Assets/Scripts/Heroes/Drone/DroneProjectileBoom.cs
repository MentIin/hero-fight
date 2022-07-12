using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneProjectileBoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        Destroy(this.gameObject, 0.18f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hero")){
            //other.GetComponent<HeroMovement>().GetDamage(1);
        }
    }
}
