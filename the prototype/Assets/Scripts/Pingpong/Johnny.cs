using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Johnny : MonoBehaviour
{
    public GameObject johnnyGameObject;
    public Animator animator;

    public GameObject ball;
    public bool turnOffBall=false;
    public bool dead=false;
    public bool talking=false;



    void Start()
    {
        Debug.Log("Start");
        animator.SetBool("talking",talking);
        if(talking) StartCoroutine(SetPlaybackTime());
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        Debug.Log("trigger enter");
        
        if(other.gameObject.tag=="PlayerSword"){
            if(johnnyGameObject!=null){
                johnnyGameObject.SetActive(true);
                GetComponent<SkinnedMeshRenderer>().enabled=false;
            }
            animator.SetTrigger("death");
            if(talking){
                GetComponent<AudioSource>().Stop();
            }
            dead=true;
            if(turnOffBall){
                bool killball=true;
                foreach(Johnny j in FindObjectsOfType<Johnny>()){
                    if(j.turnOffBall && !j.dead){
                        killball=false;
                    }
                }
                if(killball){
                    ball.SetActive(false);
                    foreach(TriggerCamera tc in FindObjectsOfType<TriggerCamera>()){
                        tc.killedPingpong=true;
                    }
                    GameObject.Find("Airplane Sound").GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    IEnumerator SetPlaybackTime(){
        yield return new WaitForSeconds(1f);
        animator.playbackTime=Random.Range(0f,2f);
    }

    void OnCollisionEnter(Collision collision){
        Debug.Log("Collsion");
    }
}
