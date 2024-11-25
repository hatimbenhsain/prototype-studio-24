using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class Flick : MonoBehaviour
{
public Animator[] hand1;
public Animator[] hand2;

public float timer1=0f;
public float timer2=0f;

public float flickTime=0.3f;

public BoxCollider hand1Box;
public BoxCollider hand2Box;

public Sculpture sculpture;
public float minSculptureDistance=3f;

public AudioClip[] clips;
private AudioSource audioSource;

public Volume volume;
private Bloom bloom;

private float bloomTimer=0f;

    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        if(volume!=null){
            //bloom=profile.GetComponent<Bloom>();
            volume.profile.TryGet(out bloom);
        }

    }

    // Update is called once per frame
    void Update()
    {
        bloomTimer+=Time.deltaTime;
        if(bloom!=null){
            bloom.intensity.value=Mathf.Min(bloomTimer,12f);
        }

        timer1+=Time.deltaTime;
        timer2+=Time.deltaTime;

        if(Input.GetMouseButtonDown(0) && timer1>=flickTime){
            for(int i=0;i<hand1.Length;i++){
                hand1[i].SetTrigger("flick");
            }
            timer1=0f;
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Cloth");
            Collider[] colliders=Physics.OverlapBox(hand1Box.transform.position+hand1Box.center,
            new Vector3(hand1Box.size.x*hand1Box.transform.lossyScale.x,hand1Box.size.y*hand1Box.transform.lossyScale.y,hand1Box.size.z*hand1Box.transform.lossyScale.z),
            hand1Box.transform.rotation,mask,QueryTriggerInteraction.Collide);
            if(colliders.Length>0){
                for(int k=0;k<colliders.Length;k++){
                    colliders[k].GetComponentInChildren<FacePlayer>().enabled=!colliders[k].GetComponentInChildren<FacePlayer>().enabled;
                    colliders[k].GetComponent<Animator>().SetTrigger("dance");
                }
                audioSource.clip=clips[Random.Range(0,clips.Length)];
                audioSource.Play();
                bloomTimer=0f;
            }else{
            }

            if(sculpture!=null && Vector3.Distance(sculpture.transform.position,transform.position)<=minSculptureDistance){
                sculpture.Triggered();
                audioSource.clip=clips[Random.Range(0,clips.Length)];
                audioSource.Play();
                FindObjectOfType<FirstPersonDrifter>().Respawn();
            }
        }
        if(Input.GetMouseButtonDown(1) && timer2>=flickTime){
            for(int i=0;i<hand1.Length;i++){
                hand2[i].SetTrigger("flick");
            }
            timer2=0f;
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Cloth");
            Collider[] colliders=Physics.OverlapBox(hand2Box.transform.position+hand2Box.center,
            new Vector3(hand2Box.size.x*hand2Box.transform.lossyScale.x,hand2Box.size.y*hand2Box.transform.lossyScale.y,hand2Box.size.z*hand2Box.transform.lossyScale.z),
            hand2Box.transform.rotation,mask,QueryTriggerInteraction.Collide);
            if(colliders.Length>0){
                for(int k=0;k<colliders.Length;k++){
                    colliders[k].GetComponentInChildren<FacePlayer>().enabled=!colliders[k].GetComponentInChildren<FacePlayer>().enabled;
                    colliders[k].GetComponent<Animator>().SetTrigger("dance");
                }                
                audioSource.clip=clips[Random.Range(0,clips.Length)];
                audioSource.Play();
                bloomTimer=0f;
            }else{
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(hand1Box.bounds.center,hand1Box.size*hand1Box.transform.localScale.x);
    }
}
