using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private float y;
    private float yOrig;
    private float amplintude;
    public ParticleSystem particles;
    private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        y = transform.position.y;
        yOrig = y;
        amplintude = 0.13f;
        GameManager.S.bonusesCount += 1;
    }

    // Update is called once per frame
    void Update()
    {
        y = yOrig + Mathf.Sin(Time.time * 2.5f) * amplintude;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hero")){
            ActivateBonus(other.gameObject);
            Destroy(this.gameObject);
            GameManager.S.bonusesCount -= 1;
            Destroy(parent, 3);
            particles.Stop();
        }
    }
    virtual protected void ActivateBonus(GameObject hero){

    }
}
