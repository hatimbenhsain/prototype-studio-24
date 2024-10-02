using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflection : MonoBehaviour
{
    public GameObject thingToReflect;
    private Vector3 offset;
    void Start()
    {
        offset=transform.position-thingToReflect.transform.position;
    }

    void Update()
    {
        Vector3 pos=thingToReflect.transform.position-offset;
        transform.position=offset+new Vector3(pos.x,-pos.y,pos.z);
    }
}
