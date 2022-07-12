using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    public Sprite[] sprites;

    private SpriteRenderer _sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();

        SetRandomSprite();
        Destroy(this.gameObject, 1f);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetRandomSprite()
    {
        _sprite.sprite = sprites[Random.Range(0, sprites.Length)];
    }
    
}
