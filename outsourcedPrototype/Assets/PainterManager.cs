using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainterManager : MonoBehaviour
{
    public Painter[] allPainter;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Painter p in allPainter) 
        {
            p.PaintTarget();
        }
    }
}
