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
    void Start()
    {
        Debug.Log("Start");
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
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision){
        Debug.Log("Collsion");
    }
}
