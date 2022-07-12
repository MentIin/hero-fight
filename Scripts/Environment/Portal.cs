using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform target;
    public bool x=true;
    public bool y=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 pos = other.transform.position;
        if (x) pos = new Vector3(target.position.x, pos.y, pos.z);
        if (y) pos = new Vector3(pos.x, target.position.y, pos.z);
        other.transform.position = pos;
    }
}
