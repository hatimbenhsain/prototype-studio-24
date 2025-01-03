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

    private Sprite initialSprite;

    void Start()
    {
        if(!TryGetComponent<SpriteRenderer>(out spriteRenderer)){
            spriteRenderer=GetComponentInChildren<SpriteRenderer>();
        }
        offset=Random.Range(0f,time);
        timer+=offset;
        
        initialSprite=spriteRenderer.sprite;
        spriteRenderer.sprite=sprites[Random.Range(0,sprites.Length)];

    }

    void Update()
    {
        timer+=Time.deltaTime;
        if(time>0f && timer>=time){
            spriteRenderer.sprite=sprites[Random.Range(0,sprites.Length)];
        }   
        timer=timer%time;
    }

    public void Change(){
        timer=time;
    }

    public void Reset(){
        spriteRenderer.sprite=initialSprite;
    }
}
