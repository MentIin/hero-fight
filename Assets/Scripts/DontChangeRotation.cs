using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontChangeRotation : MonoBehaviour
{
    private Quaternion defoultRotation;

    void Awake()
    {
        defoultRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = defoultRotation;
    }
}
