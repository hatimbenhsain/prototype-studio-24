using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerCamera : MonoBehaviour
{
    public GameObject camera;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            camera.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag=="Player"){
            camera.SetActive(false);
        }
    }
}
