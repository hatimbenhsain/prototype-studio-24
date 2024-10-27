using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DoorLock : MonoBehaviour
{
    public Image[] images;
    public RawImage rawImage;
    public float targetOpacity=0f;
    public float lerpSpeed=1f;
    public float opacity=0f;

    public AudioSource[] audioSources;
    public float[] maxVolumes;
    public float[] targetVolumes;

    public float audioLerpSpeed=5f;

    public float shutUpTimer=0f;
    public float shutUpTime=1f;
    public float speakUpTime=3f;
    public bool playerHere=false;

    void Start()
    {
        audioSources=transform.parent.GetComponentsInChildren<AudioSource>();
        maxVolumes=new float[audioSources.Length];
        targetVolumes=new float[audioSources.Length];
        for(int i=0;i<audioSources.Length;i++){
            maxVolumes[i]=audioSources[i].volume;
            targetVolumes[i]=audioSources[i].volume;
        }
    }

    void Update()
    {
        opacity=Mathf.Lerp(opacity,targetOpacity,Time.deltaTime*lerpSpeed);
        Color c;
        foreach(Image img in images){
            c=img.color;
            c.a=opacity;
            img.color=c;
        }
        c=rawImage.color;
        c.a=opacity;
        rawImage.color=c;

        shutUpTimer+=Time.deltaTime;
        if(playerHere && shutUpTimer>=shutUpTime){
            for(int i=0;i<audioSources.Length;i++){
                targetVolumes[i]=0f;
            }
        }else if(!playerHere && shutUpTimer>=speakUpTime){
            for(int i=0;i<audioSources.Length;i++){
                targetVolumes[i]=maxVolumes[i];
            }
        }

        for(int i=0;i<audioSources.Length;i++){
            audioSources[i].volume=Mathf.Lerp(audioSources[i].volume,targetVolumes[i],Time.deltaTime*audioLerpSpeed);
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            playerHere=true;
            targetOpacity=1f;
            shutUpTimer=0f;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag=="Player"){
            playerHere=false;
            targetOpacity=0f;
            shutUpTimer=0f;
        }
    }
}
