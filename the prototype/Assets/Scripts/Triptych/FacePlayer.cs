using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    private Transform cameraTransform;

    public bool lockX=false;
    public bool lockY=false;
    public bool lockZ=false;

    private float ogX;
    private float ogY;
    private float ogZ;
    void Start()
    {
        Vector3 rot=transform.rotation.eulerAngles;
        ogX=rot.x;
        ogY=rot.y; 
        ogZ=rot.z;
    }

    void Update()
    {
        cameraTransform=Camera.main.transform;

        Vector3 rot=cameraTransform.rotation.eulerAngles;

        if(lockX){
            rot.x=ogX;
        }
        if(lockY){
            rot.y=ogY;
        }
        if(lockZ){
            rot.z=ogZ;
        }

        transform.rotation=Quaternion.Euler(rot);
    }
}
