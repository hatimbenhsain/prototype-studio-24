using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flick : MonoBehaviour
{
public Animator[] hand1;
public Animator[] hand2;

public float timer1=0f;
public float timer2=0f;

public float flickTime=0.3f;

public BoxCollider hand1Box;
public BoxCollider hand2Box;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer1+=Time.deltaTime;
        timer2+=Time.deltaTime;

        if(Input.GetMouseButtonDown(0) && timer1>=flickTime){
            for(int i=0;i<hand1.Length;i++){
                hand1[i].SetTrigger("flick");
            }
            timer1=0f;
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Cloth");
            Collider[] colliders=Physics.OverlapBox(hand1Box.transform.position+hand1Box.center,hand1Box.size*hand1Box.transform.lossyScale.x,hand1Box.transform.rotation,mask,QueryTriggerInteraction.Collide);
            if(colliders.Length>0){
                for(int k=0;k<colliders.Length;k++){
                    Destroy(colliders[k].gameObject);
                }                
            }else{
            }
        }
        if(Input.GetMouseButtonDown(1) && timer2>=flickTime){
            for(int i=0;i<hand1.Length;i++){
                hand2[i].SetTrigger("flick");
            }
            timer2=0f;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(hand1Box.bounds.center,hand1Box.size*hand1Box.transform.localScale.x);
    }
}
