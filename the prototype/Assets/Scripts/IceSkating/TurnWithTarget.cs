using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnWithTarget : MonoBehaviour
{
    public Transform target;
    private Vector3 initialRotation;
    private Vector3 rotationOffset;
     void Start()
    {
        initialRotation=transform.rotation.eulerAngles;
        rotationOffset=target.rotation.eulerAngles-initialRotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation=Quaternion.Euler(target.rotation.eulerAngles-rotationOffset);
    }
}
