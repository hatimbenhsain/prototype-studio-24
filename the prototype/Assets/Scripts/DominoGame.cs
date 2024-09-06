using System.Collections;
using System.Collections.Generic;
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

    void Awake()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !dominoPushed){
            hand.SetActive(true);
            hand.GetComponent<DominoHand>().Push();
        }
    }

    public void Explosion(Vector3 pos){
        if(!explosionHappened){
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
        }
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
}
