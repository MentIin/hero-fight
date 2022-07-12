using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnClouds : MonoBehaviour
{
    public GameObject[] clouds;
    public float spawnRate=5f;
    public float maxY=10f;
    public float minY=-10f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnCloudStart();
        SpawnCloud();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnCloudStart(){
        for (int i=0; i<Random.Range(1, 5);i++){
            float y = Random.Range(minY, maxY);
            float x = Random.Range(-10, 10);
            Instantiate(clouds[Random.Range(0, clouds.Length)], new Vector3(x, y, 0), Quaternion.identity);
        }
    }
    void SpawnCloud(){
        float y = Random.Range(minY, maxY);
        float x = 10;
        Instantiate(clouds[Random.Range(0, clouds.Length)], new Vector3(x, y, 0), Quaternion.identity);
        Invoke("SpawnCloud", Random.Range(10, 100) / spawnRate);
    }
}
