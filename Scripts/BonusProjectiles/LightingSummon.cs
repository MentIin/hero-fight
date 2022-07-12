using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSummon : MonoBehaviour
{
    public GameObject lightingProjectile;

    public Quaternion rotation;

    public bool red;

    private float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 0.2f)
        {
            Spawn();
        }

    }
    public void Spawn(){
        GameObject projectile = Instantiate(lightingProjectile, transform.position, rotation);
        projectile.GetComponent<LightingProjectile>().red = red;
        
        DestroyImmediate(this.gameObject);
    }
}
