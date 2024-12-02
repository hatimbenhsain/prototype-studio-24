using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    public GameObject brush;

    public Transform topLeftBoundary;
    public Transform bottomRightBoundary;

    public float minDistance=0.01f;
    private Vector3 prevDrawnPosition;

    public MouseLook[] mouseLooks;

    public List<SpriteRenderer> strokes;

    private float targetVolume=0f;

    public float volumeChangeSpeed=10f;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible=false;

        mouseLooks=FindObjectsOfType<MouseLook>();

        strokes=new List<SpriteRenderer>();

        audioSource=GetComponent<AudioSource>();
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

        targetVolume=Mathf.Lerp(targetVolume,0f,Time.deltaTime*volumeChangeSpeed);

        if(Input.GetMouseButton(0)){
            if(brushDistance>=minDistance){
                targetVolume=1f;
                Vector3 difference=brush.transform.localPosition-prevDrawnPosition;
                for(var i=0;i<Mathf.Floor(brushDistance/minDistance);i++){
                    GameObject stroke=Instantiate(brush,brush.transform.parent);
                    strokes.Add(stroke.GetComponent<SpriteRenderer>());
                    stroke.transform.localPosition=brush.transform.localPosition+difference*i/Mathf.Floor(brushDistance/minDistance);
                    stroke.transform.localRotation=Quaternion.Euler(new Vector3(stroke.transform.localRotation.eulerAngles.x,stroke.transform.localRotation.eulerAngles.y,Random.Range(0f,360f)));
                }                
                prevDrawnPosition=brush.transform.localPosition;
            }
            
        }

        audioSource.volume=targetVolume;
        
        if(Input.GetMouseButton(0) || Input.GetMouseButton(1)){
            foreach(MouseLook mouseLook in mouseLooks){
                mouseLook.enabled=false;
            }
            Cursor.lockState=CursorLockMode.None;
        }else{
            foreach(MouseLook mouseLook in mouseLooks){
                mouseLook.enabled=true;
            }
            Cursor.lockState=CursorLockMode.Locked;
        }

        if(Input.GetKeyDown(KeyCode.R)){
            StartCoroutine(Erase());
            strokes.Clear();
        }
        
    }

    IEnumerator Erase(){
        SpriteRenderer[] myStrokes=new SpriteRenderer[strokes.Count];
        strokes.CopyTo(myStrokes);
        Color c=myStrokes[0].color;
        int tries=0;
        while(c.a>=0 && tries<500){
            c.a=c.a-0.01f;
            foreach(SpriteRenderer s in myStrokes){
                s.color=c;
            }
            yield return new WaitForSeconds(0.05f);
            tries++;
        }
        foreach(SpriteRenderer s in myStrokes){
            Destroy(s.gameObject);
        }
    }
}
