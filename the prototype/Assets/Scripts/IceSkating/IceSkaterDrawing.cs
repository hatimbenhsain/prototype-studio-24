using System.Collections;
using System.Collections.Generic;
using Shapes;
using UnityEngine;

public class IceSkaterDrawing : MonoBehaviour
{
    ArrayList points;
    Vector3 prevPosition;
    float timer;
    public float minDistance=0.1f;

    public GameObject linePrefab;

    public float yValue=0.01f;

    private IceSkater iceSkater;

    public float minVelocity=0.1f;
    public float maxVelocity=5f;

    public float minAlpha=0.1f;
    public float maxAlpha=0.5f;

    private ArrayList lines;

    public float maxLines=1000f;


    void Start()
    {
        points=new ArrayList();
        prevPosition=transform.position;
        prevPosition.y=yValue;
        iceSkater=GetComponentInParent<IceSkater>();
        timer=0;
        lines=new ArrayList();
    }

    void Update()
    {
        Vector3 currentPosition=transform.position;
        currentPosition.y=yValue;
        timer+=Time.deltaTime;
        float vel=Vector3.Distance(currentPosition,prevPosition)/timer;
        if(!iceSkater.grounded || vel==0){
            prevPosition=currentPosition;
            timer=0f;
        }else if(iceSkater.grounded && Vector3.Distance(currentPosition,prevPosition)>=minDistance ){          
            if(lines.Count>=maxLines){
                Destroy(((Line)lines[0]).gameObject);
                lines.RemoveAt(0);
            }

            points.Add(transform.position);
            var obj=Instantiate(linePrefab);
            Line line=obj.GetComponent<Line>();
            line.Start=prevPosition;
            line.End=currentPosition;
            //float vel=GetComponent<CharacterController>().velocity.magnitude;
            Debug.Log(vel);
            float alpha=minAlpha+(maxAlpha-minAlpha)*Mathf.Clamp((vel-minVelocity)/(maxVelocity-minVelocity),0f,1f);
            Debug.Log(alpha);
            Color c=line.Color;
            c.a=alpha;
            line.Color=c;
            prevPosition=currentPosition;
            timer=0f;
            lines.Add(line);
        }
    }
}

