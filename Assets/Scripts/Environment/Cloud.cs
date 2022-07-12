using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float moveDistance=0.2f;
    public float moveDelay=1f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", moveDelay, moveDelay);
    }
    void Move(){
        transform.Translate(Vector3.left * moveDistance);
    }

    void OnBecameInvisible() {
        Destroy(this.gameObject);
    }
}
