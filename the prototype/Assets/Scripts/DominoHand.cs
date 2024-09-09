using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoHand : MonoBehaviour
{
    public Camera camera;

    private bool pushing=false;

    public float speed=1f;

    public float timeBeforeEnd=4f;
    public float cameraTime=0.25f;

    public Material transparentMaterial;
    public Material opaqueMaterial;
    public GameObject spotlight;


    private Rigidbody body;
    // Start is called before the first frame update
    private void Awake()
    {
        body=GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pushing && camera.gameObject.activeInHierarchy && FindObjectOfType<DominoGame>().explosionHappenedOnce){
            StartCoroutine(ToggleCamera(false,0f));
        }
    }

    private void FixedUpdate() {
        if(pushing){
            transform.position+=transform.forward*speed*Time.deltaTime;
            //body.MovePosition(transform.position+Vector3.forward*speed*Time.fixedDeltaTime);
        }
    }

    public void Push(){
        pushing=true;
        camera.gameObject.SetActive(false);
        StartCoroutine(ToggleCamera(true,cameraTime));
        StartCoroutine(StopPushing(timeBeforeEnd));
        StartCoroutine(ToggleCamera(false,timeBeforeEnd-cameraTime));
        Invoke("SetTransparent",timeBeforeEnd-cameraTime);
        SetOpaque();
    }

    private IEnumerator StopPushing(float waitTime){
        yield return new WaitForSeconds(waitTime);
        pushing=false;
        gameObject.SetActive(false);   
    }

    private IEnumerator ToggleCamera(bool b, float waitTime){
        yield return new WaitForSeconds(waitTime);
        camera.gameObject.SetActive(b);
        FindObjectOfType<DominoGame>().toggleSpotlight(!b);
        spotlight.SetActive(b);
    }


    public void SetTransparent(){
        GetComponentInChildren<MeshRenderer>().material=transparentMaterial;
        body.detectCollisions=false;
    }

    public void SetOpaque(){
        GetComponentInChildren<MeshRenderer>().material=opaqueMaterial;
        body.detectCollisions=true;
    }

    // MAKE HAND STOP PUSHING RIGHT AFTER EXPLOSION AND BECOME TRANSPARENT/INTANGIBLE
}
