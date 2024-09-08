using System.Collections;
using System.Collections.Generic;
using DoodleStudio95;
using UnityEngine;

public class DominoGame : MonoBehaviour
{
    public bool explosionHappened=false;

    public float distanceFactor=1f;
    public float explosionForce=1f;

    public bool dominoPushed=false;
    public GameObject hand;

    public float slowTimescale=0.25f;
    public float slowmoDuration=4f;

    private float fixedDeltaTime;

    private DominoScript[] dominoes;
    private DominoScript dominoHovered;

    enum State {Looking, Pushing}

    private State currentState;

    private bool dominoFront=true;

    public GameObject explosionPrefab;

    public GameObject spotlight;

    void Awake()
    {
        fixedDeltaTime = Time.fixedDeltaTime;

        dominoes=FindObjectsOfType<DominoScript>();

        currentState=State.Looking;
    }

    void Update()
    {

        RaycastHit hit;
        Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);

        if(currentState==State.Looking){
            if(Input.GetKeyDown(KeyCode.Tab)){
                dominoFront=!dominoFront;
            }

            foreach(DominoScript d in dominoes){
                d.NotHovered();
            }

            if(Physics.Raycast(ray, out hit)) {
                Transform objectHit = hit.transform;
                if(objectHit.gameObject.tag=="Domino"){
                    DominoScript domino=objectHit.GetComponentInChildren<DominoScript>();
                    domino.Hovered(dominoFront);
                    dominoHovered=domino;
                }
            }else{
                dominoHovered=null;
            }

            if(Input.GetKeyDown(KeyCode.Mouse0) && dominoHovered!=null && !dominoPushed){
                dominoHovered.Push(dominoFront);
                currentState=State.Pushing;
            }
        }

        explosionHappened=false;
    }

    public void Explosion(Vector3 pos){
        if(!explosionHappened && Time.timeSinceLevelLoad>1f){
            explosionHappened=true;
            Rigidbody[] bodies=FindObjectsOfType<Rigidbody>();
            foreach(Rigidbody b in bodies){
                if(!b.isKinematic){
                    float d=Vector3.Distance(pos,b.position);
                    Vector3 force=(b.position-pos).normalized;
                    force=force*explosionForce*(Mathf.Max(1-d*distanceFactor,0f));
                    b.AddForce(force,ForceMode.Impulse);
                }
            }
            ChangeTimeScale(slowTimescale);
            StartCoroutine(RestoreTimeScale(slowmoDuration*slowTimescale));
            StartCoroutine(PlayExplosion(pos));
            GetComponent<AudioSource>().Play();
        }
    }

    IEnumerator PlayExplosion(Vector3 pos) {
        GameObject explosion=Instantiate(explosionPrefab,pos,Quaternion.identity);
        yield return explosion.GetComponent<DoodleAnimator>().PlayAndPauseAt(0,-1);
    }

    private IEnumerator RestoreTimeScale(float waitTime){
        yield return new WaitForSeconds(waitTime);
        ChangeTimeScale(1f);
    }

    public void ChangeTimeScale(float timeScale){
        Debug.Log("change time scale");
        Debug.Log(timeScale);
        Time.timeScale=timeScale;
        Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
    }

    public void toggleSpotlight(bool b){
        spotlight.SetActive(b);
    }
}
