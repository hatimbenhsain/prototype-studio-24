using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoHand : MonoBehaviour
{
    private Camera camera;

    private bool pushing=false;

    public float speed=1f;

    public float timeBeforeEnd=4f;
    public float cameraTime=0.25f;

    private Rigidbody body;
    // Start is called before the first frame update
    private void Awake()
    {
        camera=GetComponentInChildren<Camera>();
        body=GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

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
    }

    private IEnumerator StopPushing(float waitTime){
        yield return new WaitForSeconds(waitTime);
        pushing=false;
        // gameObject.SetActive(false);   
    }

    private IEnumerator ToggleCamera(bool b, float waitTime){
        yield return new WaitForSeconds(waitTime);
        camera.gameObject.SetActive(b);  
    }

    // MAKE HAND STOP PUSHING RIGHT AFTER EXPLOSION AND BECOME TRANSPARENT/INTANGIBLE
}
