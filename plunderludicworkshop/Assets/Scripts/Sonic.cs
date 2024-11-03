using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonic : MonoBehaviour
{
    private FirstPersonDrifter player;

    public Transform sonicEmulator;
    public Transform sprite;

    public float minDistance=8f;
    // Start is called before the first frame update
    void Start()
    {
        player=FindObjectOfType<FirstPersonDrifter>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position,transform.position)<=minDistance){
            sonicEmulator.position=sprite.position;
        }
    }
}
