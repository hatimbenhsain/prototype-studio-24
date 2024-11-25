using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sculpture : MonoBehaviour
{
    public bool triggered=false;

    public float triggeredTimer=0f;
    public float triggeredTime=2f;

    private Vector3 initialScale;

    public float frequency=0.1f;

    public float bufferTimer=0f;
    public float bufferTime=1f;

    private int triggeredCount=0;

    private RandomizeSprite[] randomizeSprites;
    void Start()
    {
        randomizeSprites=GetComponentsInChildren<RandomizeSprite>();
    }

    // Update is called once per frame
    void Update()
    {
        if(triggered){
            triggeredTimer-=Time.deltaTime;
            if(triggeredTimer>=0f){
                if(bufferTimer<=0f){
                    if(Random.Range(0f,1f)<frequency){
                        transform.localScale=new Vector3(Random.Range(0,5f),Random.Range(0,5f),Random.Range(0,5f));
                        GetComponent<Animator>().enabled=false;
                        bufferTimer=bufferTime;
                        foreach(RandomizeSprite r in randomizeSprites){
                            r.Reset();
                        }
                    }else{
                        GetComponent<Animator>().enabled=true;
                    }
                }else{
                    bufferTimer-=Time.deltaTime;
                    if(bufferTimer<=0f){
                        foreach(RandomizeSprite r in randomizeSprites){
                            r.Change();
                        }
                    }
                }
            }else{
                triggered=false;
                transform.localScale=initialScale;
                GetComponent<Animator>().enabled=true;
                GetComponent<AudioSource>().Stop();
                
            }
        }
    }

    public void Triggered(){
        if(!triggered){
            triggered=true;
            triggeredTimer=triggeredTime;
            initialScale=transform.localScale;
            triggeredCount+=1;
            if(triggeredCount>=5){
                FindObjectOfType<Triptych>().StartCoroutine("GoNext",0f);
            }else{
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
