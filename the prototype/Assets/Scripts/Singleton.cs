using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Singleton : MonoBehaviour
{
    void Awake()
    {
        foreach(var singleton in FindObjectsOfType<Singleton>())
        {   
            if(singleton.gameObject.name==gameObject.name && singleton.gameObject!=gameObject)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
