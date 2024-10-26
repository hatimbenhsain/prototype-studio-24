using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorLock : MonoBehaviour
{
    public Image[] images;
    public RawImage rawImage;
    public float targetOpacity=0f;
    public float lerpSpeed=1f;
    public float opacity=0f;

    void Start()
    {
        
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
    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            targetOpacity=1f;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag=="Player"){
            targetOpacity=0f;
        }
    }
}
