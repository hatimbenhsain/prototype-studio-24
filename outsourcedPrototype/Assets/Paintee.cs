using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintee : Painter
{
    public float myHealth = 100000;
    public bool onFire;
    // Start is called before the first frame update
    void Start()
    {
        Renderer myRend = GetComponent<Renderer>();
        myRend.material.color = myCol;
    }

    // Update is called once per frame
    void Update()
    {
        nearestPoint = meshCol.ClosestPoint(transform.position);
        VerticesInRange(nearestPoint);

        for (int i = 0; i < vertices.Length; i++)
        {
            onFire = false;
            if (inRange[i] && mesh.colors[i] != myCol)
            {
                onFire = true; goto LoopEnd;
            }
        }
        LoopEnd:
        if(onFire) { myHealth--; }
    }
}
