using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldsEffect : MonoBehaviour
{
    public Transform target;
    private float rotateSpeed=50f;
    private float lifeTime=12f;
    public bool red;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
        red = target.GetComponent<HeroMovement>().red;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position;
        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
    }
}
