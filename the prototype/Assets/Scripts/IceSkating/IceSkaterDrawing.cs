using System.Collections;
using System.Collections.Generic;
using Shapes;
using UnityEngine;

public class IceSkaterDrawing : MonoBehaviour
{
    ArrayList points;
    Vector3 prevPosition;
    public float minDistance=0.1f;

    public GameObject linePrefab;

    public float yValue=0.01f;

    private IceSkater iceSkater;

    public float minVelocity=0.1f;
    public float maxVelocity=5f;

    public float minAlpha=0.1f;
    public float maxAlpha=0.5f;



    void Start()
    {
        points=new ArrayList();
        prevPosition=transform.position;
        prevPosition.y=yValue;
        iceSkater=GetComponentInParent<IceSkater>();
    }

    void Update()
    {
        Vector3 currentPosition=transform.position;
        currentPosition.y=yValue;
        if(iceSkater.grounded && Vector3.Distance(transform.position,prevPosition)>=minDistance ){
            points.Add(transform.position);
            var obj=Instantiate(linePrefab);
            Line line=obj.GetComponent<Line>();
            line.Start=prevPosition;
            line.End=currentPosition;
            float vel=Vector3.Distance(currentPosition,prevPosition)/Time.deltaTime;
            //float vel=GetComponent<CharacterController>().velocity.magnitude;
            Debug.Log(vel);
            float alpha=minAlpha+(maxAlpha-minAlpha)*Mathf.Clamp((vel-minVelocity)/(maxVelocity-minVelocity),0f,1f);
            Debug.Log(alpha);
            Color c=line.Color;
            c.a=alpha;
            line.Color=c;
            prevPosition=currentPosition;
        }else if(!iceSkater.grounded){
            prevPosition=currentPosition;
        }
    }
}

