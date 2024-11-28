using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
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
    public float zoomoutAcceleration=5f;

    public AudioMixer mixer;

    public GameObject endScreen;
    private Image endScreen2;

    private DepthOfField depthOfField;
    private ColorAdjustments colorAdjustments;
    private Bloom bloom;

    void Start()
    {
        airplaneWindow=GameObject.Find("Window").GetComponent<Image>();
        endScreen2=GameObject.Find("EndScreen 2").GetComponent<Image>();
        
    }

    void Update()
    {
        if(stopChanging && camera.activeInHierarchy){
            airplaneWindowTimer+=Time.deltaTime;
            Color c=airplaneWindow.color;
            c.a=airplaneWindowTimer/airplaneWindowTime;
            // airplaneWindow.color=c;
            zoomoutSpeed+=zoomoutAcceleration*Time.deltaTime;
            //camera.GetComponent<Camera>().orthographicSize=camera.GetComponent<Camera>().orthographicSize+Time.deltaTime*zoomoutSpeed;
            FindObjectOfType<FirstPersonDrifter>().gameObject.GetComponent<AudioSource>().volume-=1f*Time.deltaTime;
            float v;
            mixer.GetFloat("volume",out v);
            mixer.SetFloat("volume",v-Time.deltaTime);
            depthOfField.focalLength.value=airplaneWindowTimer;
            bloom.intensity.value=4.25f+airplaneWindowTimer;
            colorAdjustments.contrast.value=20f+airplaneWindowTimer*80f/60f;
            if(airplaneWindowTimer>=60f){
                endScreen.SetActive(true);
            } if(airplaneWindowTimer>=62f){
                endScreen2.color=new Color(1f,1f,1f,1f);
            } if(airplaneWindowTimer>=63f){
                endScreen.SetActive(false);
                airplaneWindow.color=new Color(0,0,0,1f);
                GameObject.Find("Airplane Sound").GetComponent<AudioSource>().Stop();
                Application.Quit();
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(!stopChanging && !killedPingpong){
            if(other.tag=="Player"){
                camera.SetActive(true);
            }
            // if(killedPingpong){
            //     foreach(TriggerCamera tc in FindObjectsOfType<TriggerCamera>()){
            //         tc.camera.SetActive(false);
            //         tc.stopChanging=true;
            //     }
            //     camera.SetActive(true);
            // }
        }
        
    }

    void OnTriggerExit(Collider other){
        if(!stopChanging){
            if(killedPingpong){
                foreach(TriggerCamera tc in FindObjectsOfType<TriggerCamera>()){
                    tc.camera.SetActive(false);
                    tc.stopChanging=true;
                }
                camera.SetActive(true);
                GameObject.Find("Global Volume").GetComponent<Volume>().profile.TryGet<DepthOfField>(out depthOfField);
                GameObject.Find("Global Volume").GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out colorAdjustments);
                GameObject.Find("Global Volume").GetComponent<Volume>().profile.TryGet<Bloom>(out bloom);
            }else if(other.tag=="Player"){
                camera.SetActive(false);
            }
        }
    }
}
