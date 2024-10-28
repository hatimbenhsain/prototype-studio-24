using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TriggerCamera : MonoBehaviour
{
    public GameObject camera;
    public bool killedPingpong;
    public bool stopChanging;

    public Image airplaneWindow;
    public float airplaneWindowTimer=0f;
    public float airplaneWindowTime=10f;

    private float zoomoutSpeed=0f;
    private float zoomoutAcceleration=5f;

    public AudioMixer mixer;

    void Start()
    {
        airplaneWindow=GameObject.Find("Window").GetComponent<Image>();
    }

    void Update()
    {
        if(stopChanging && camera.activeInHierarchy){
            airplaneWindowTimer+=Time.deltaTime;
            Color c=airplaneWindow.color;
            c.a=airplaneWindowTimer/airplaneWindowTime;
            airplaneWindow.color=c;
            zoomoutSpeed+=zoomoutAcceleration*Time.deltaTime;
            camera.GetComponent<Camera>().orthographicSize=camera.GetComponent<Camera>().orthographicSize+Time.deltaTime*zoomoutSpeed;
            FindObjectOfType<FirstPersonDrifter>().gameObject.GetComponent<AudioSource>().volume-=1f*Time.deltaTime;
            float v;
            mixer.GetFloat("volume",out v);
            mixer.SetFloat("volume",v-Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other){
        if(!stopChanging){
            if(other.tag=="Player"){
                camera.SetActive(true);
            }
            if(killedPingpong){
                foreach(TriggerCamera tc in FindObjectsOfType<TriggerCamera>()){
                    tc.stopChanging=true;
                }
            }
        }
        
    }

    void OnTriggerExit(Collider other){
        if(!stopChanging){
            if(other.tag=="Player"){
                camera.SetActive(false);
            }
        }
    }
}
