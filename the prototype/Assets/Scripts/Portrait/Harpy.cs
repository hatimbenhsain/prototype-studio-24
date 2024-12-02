using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Harpy : MonoBehaviour
{
    public Transform atama;
    public Transform player;

    public float atamaXMin=-90f;
    public float atamaXMax=90f;
    public float atamaYMin=-90f;
    public float atamaYMax=36f;
    public float atamaZMin=-55f;
    public float atamaZMax=55f;

    public Transform[] roostingPlaces;

    public Transform currentRoostingPlace;

    public float maxDrawingTime=5f;
    public float minPlayerDistance=10f;

    public float annoyedTimer=0f;

    public bool moving=false;

    public float movingSpeed=10f;
    public float turningSpeed=10f;

    public Volume volume;

    private ShadowsMidtonesHighlights shadowsMidtonesHighlights;
    private ColorAdjustments colorAdjustments;

    public AudioSource wingsSource;

    void Start()
    {
        player=GameObject.FindWithTag("Player").transform;

        volume.profile.TryGet<ShadowsMidtonesHighlights>(out shadowsMidtonesHighlights);
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);

        shadowsMidtonesHighlights.active=false;
        colorAdjustments.active=false;
    }

    void Update()
    {
        if(!moving){
            if(Vector3.Distance(player.position,transform.position)<=minPlayerDistance && Input.GetMouseButton(0)){
                annoyedTimer+=Time.deltaTime;
            }else{
                annoyedTimer-=Time.deltaTime;
            }
        }

        annoyedTimer=Mathf.Clamp(annoyedTimer,0f,maxDrawingTime);

        if(annoyedTimer>=maxDrawingTime){
            Transform crp=currentRoostingPlace;
            int tries=0;
            while(crp==currentRoostingPlace && tries<100){
                crp=roostingPlaces[Random.Range(0,roostingPlaces.Length)];
            }
            currentRoostingPlace=crp;
            GetComponent<Animator>().SetTrigger("attack");
            annoyedTimer=0;
            StartCoroutine(StartMoving());
            GetComponent<AudioSource>().pitch=Random.Range(0.95f,1.05f);
            GetComponent<AudioSource>().Play();
        }

        if(moving){
            Vector3 newPos=Vector3.Lerp(transform.position,currentRoostingPlace.position,movingSpeed*Time.deltaTime);
            Quaternion rotation=Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(currentRoostingPlace.position-transform.position,Vector3.up),turningSpeed*Time.deltaTime);
            transform.position=newPos;
            transform.rotation=rotation;
            if(Vector3.Distance(currentRoostingPlace.position,transform.position)<=0.1f){
                moving=false;
                transform.position=currentRoostingPlace.position;
                GetComponent<Animator>().SetBool("flying",false);
                annoyedTimer=maxDrawingTime-0.5f;
                wingsSource.Stop();
            }
        }else{
            Quaternion rotation=Quaternion.Lerp(transform.rotation,currentRoostingPlace.rotation,turningSpeed*Time.deltaTime);
            transform.rotation=rotation;
        }
    }

    private void LateUpdate() {
        atama.LookAt(player);
        Vector3 atamaRotation=atama.localRotation.eulerAngles;
        if(atamaRotation.x<=180f) atamaRotation.x=Mathf.Clamp(atamaRotation.x, atamaXMin, atamaXMax);
        else atamaRotation.x=Mathf.Clamp(atamaRotation.x, 360f+atamaXMin, 360f+atamaXMax);
        if(atamaRotation.y<=180f) atamaRotation.y=Mathf.Clamp(atamaRotation.y, atamaYMin, atamaYMax);
        else atamaRotation.y=Mathf.Clamp(atamaRotation.y, 360f+atamaYMin, 360f+atamaYMax);
        if(atamaRotation.z<=180f) atamaRotation.z=Mathf.Clamp(atamaRotation.z, atamaZMin, atamaZMax);
        else atamaRotation.z=Mathf.Clamp(atamaRotation.z, 360f+atamaZMin, 360f+atamaZMax);
        atama.localRotation=Quaternion.Euler(atamaRotation);
    }

    IEnumerator StartMoving(){
        yield return new WaitForSeconds(0.5f);
        moving=true;
        GetComponent<Animator>().SetBool("flying",true);
        if(shadowsMidtonesHighlights.active || Random.Range(0f,1f)<=0.2f){
            shadowsMidtonesHighlights.active=!shadowsMidtonesHighlights.active;
            colorAdjustments.active=shadowsMidtonesHighlights.active;
            colorAdjustments.contrast.value=Random.Range(-100f,100f);
        }
        wingsSource.Play();
    }
}
