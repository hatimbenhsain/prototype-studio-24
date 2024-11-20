using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite[] sprites;

    private float timer=0f;
    public float time=5f;
    private float offset=0f;

    void Start()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        offset=Random.Range(0f,time);
        timer+=offset;
    }

    void Update()
    {
        timer+=Time.deltaTime;
        if(timer>=time){
            spriteRenderer.sprite=sprites[Random.Range(0,sprites.Length)];
        }   
        timer=timer%time;
    }
}
