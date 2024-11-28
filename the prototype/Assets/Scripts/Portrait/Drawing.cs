using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    public GameObject brush;

    public Transform topLeftBoundary;
    public Transform bottomRightBoundary;

    public float minDistance=0.01f;
    private Vector3 prevDrawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible=false;
    }

    // Update is called once per frame
    void Update()
    {
        float smallerScreenUnit=Mathf.Min(Screen.width,Screen.height);
        float mouseX=(Input.mousePosition.x-Screen.width/2)/smallerScreenUnit;
        float mouseY=(Input.mousePosition.y-Screen.height/2)/smallerScreenUnit;

        float distance=Mathf.Max(Mathf.Abs(bottomRightBoundary.localPosition.x-topLeftBoundary.localPosition.x),Mathf.Abs(bottomRightBoundary.localPosition.z-topLeftBoundary.localPosition.z));

        float x=Mathf.Clamp((topLeftBoundary.localPosition.x+bottomRightBoundary.localPosition.x)/2+mouseX*(distance),topLeftBoundary.localPosition.x,bottomRightBoundary.localPosition.x);
        float y=Mathf.Clamp((topLeftBoundary.localPosition.z+bottomRightBoundary.localPosition.z)/2+mouseY*(distance),bottomRightBoundary.localPosition.z,topLeftBoundary.localPosition.z);

        brush.transform.localPosition=new Vector3(x,brush.transform.localPosition.y,y);       

        if(Input.GetMouseButtonDown(0)){
            prevDrawnPosition=brush.transform.localPosition;
        }

        float brushDistance=Vector3.Distance(prevDrawnPosition, brush.transform.localPosition);

        if(Input.GetMouseButton(0)){
            if(brushDistance>=minDistance){
                Vector3 difference=brush.transform.localPosition-prevDrawnPosition;
                for(var i=0;i<Mathf.Floor(brushDistance/minDistance);i++){
                    GameObject stroke=Instantiate(brush,brush.transform.parent);
                    //stroke.transform.parent=brush.transform.parent;
                    stroke.transform.localPosition=brush.transform.localPosition+difference*i/Mathf.Floor(brushDistance/minDistance);
                    stroke.transform.localRotation=Quaternion.Euler(new Vector3(stroke.transform.localRotation.eulerAngles.x,stroke.transform.localRotation.eulerAngles.y,Random.Range(0f,360f)));
                }                
                prevDrawnPosition=brush.transform.localPosition;
            }
            
        }

        
    }
}
