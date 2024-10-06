using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightControl : MonoBehaviour
{
    public Transform origin;
    public float maxDistance=21f;
    public float timer=0f;
    public float timeToDarkness=5f;
    public float timeToEnd=7f;


    public Light[] lights;
    public float[] lightIntensity;
    void Start()
    {
        lightIntensity=new float[lights.Length];
        for(int i=0;i<lights.Length;i++){
            lightIntensity[i]=lights[i].intensity;
        }
    }

    void Update()
    {
        if(Vector3.Distance(transform.position,origin.position)>=maxDistance){
            timer+=Time.deltaTime;
        }else{
            timer-=Time.deltaTime;
        }

        timer=Mathf.Clamp(timer,0f,timeToEnd);

        for(int i=0;i<lights.Length;i++){
            float l=lightIntensity[i]*(1-timer/timeToDarkness);
            lights[i].intensity=l;
        }

        if(timer>=timeToEnd){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
