using System.Collections;
using System.Collections.Generic;
using DoodleStudio95;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject flamePrefab;

    public GameObject spotlight;

    public float maxX=10;
    public float maxZ=5;

    public List<GameObject> dominoPresets;
    public GameObject[] quadrants;
    

    void Awake()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
        currentState=State.Looking;

        for(var i=0;i<quadrants.Length;i++){
            int k=Mathf.FloorToInt(Random.Range(0,dominoPresets.Count));
            Instantiate(dominoPresets[k],quadrants[i].transform);
            dominoPresets.RemoveAt(k);
        }

        dominoes=FindObjectsOfType<DominoScript>();
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

        if(Input.GetKeyDown(KeyCode.R)){
            ChangeTimeScale(1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
            int n=(int)Mathf.Floor(Random.Range(3f,6f));
            for(int i=0;i<n;i++){
                Vector3 fpos=new Vector3(Random.Range(-maxX,maxX),1f,Random.Range(-maxZ,maxZ));
                GameObject flame=Instantiate(flamePrefab,fpos,Quaternion.identity);
                float sc=Random.Range(0.1f,.5f);
                flame.transform.localScale=Vector3.one*sc;
            }
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
