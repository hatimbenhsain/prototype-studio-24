using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ImageChanger : MonoBehaviour
{
    public float timePassed=0f;
    public float distanceCrossed=0f;

    public float minTimePassed=2f;
    public float minDistanceCrossed=2f;

    public GameObject[] bigGroups;
    public GameObject[][] imageGroups;
    public int currentState=0;

    public Volume volumeCameras;
    public Volume volumeColors;

    public VolumeProfile[] profilesCameras;
    public VolumeProfile[] profilesColors;

    public int volumeIndex=0;

    public int currentGroup=0;
    public int[] groupOrder;
    private int groupIndex;
    private int cycle;

    void Start()
    {
        cycle=0;
        imageGroups=new GameObject[bigGroups.Length][];
        for(int i=0;i<bigGroups.Length;i++){
            GameObject bigGroup=bigGroups[i];
            int childCount=bigGroup.transform.childCount;
            GameObject[] g=new GameObject[childCount];
            for(int k=0;k<childCount;k++){
                g[k]=bigGroup.transform.GetChild(k).gameObject;
            }
            imageGroups[i]=g;
        }
        foreach(GameObject[] group in imageGroups){
            foreach(GameObject imageGroup in group){
                imageGroup.SetActive(false);
            }
        }
        groupOrder=new int[imageGroups.Length];
        for(int i=0;i<groupOrder.Length;i++){
            groupOrder[i]=i;
        }
        Shuffle(groupOrder);
        groupIndex=0;
        currentGroup=groupOrder[groupIndex];
    }

    void Shuffle(int[] array)
    {
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = Random.Range(0, n);
            int t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timePassed+=Time.deltaTime;
    }

    public void ChangeImage(){
        int k=1;
        if(cycle>=1){
            k+=1;
            if(cycle>=2){
                k+=1;
            }
        }
        k=Random.Range(1,k+1);
        if(currentState<imageGroups[currentGroup].Length){
            k=Mathf.Clamp(k,1,(imageGroups[currentGroup].Length-currentState));
        }else{
            k=Mathf.Clamp(k,1,(imageGroups[currentGroup].Length*2-currentState));
        }
        for(int i=0;i<k;i++){
            imageGroups[currentGroup][currentState%imageGroups[currentGroup].Length].SetActive(!imageGroups[currentGroup][currentState%imageGroups[currentGroup].Length].activeInHierarchy);
            currentState+=1;
            if(currentState>=imageGroups[currentGroup].Length*2){
                break;
            }
        }
        if(currentState>=imageGroups[currentGroup].Length*2){
            currentState=0;
            groupIndex+=1;
            cycle+=1;
            if(groupIndex>=imageGroups.Length){
                groupIndex=0;
                Shuffle(groupOrder);
            }
            currentGroup=groupOrder[groupIndex];
        }
        timePassed=0;
        distanceCrossed=0;
        
    }

    public void ChangeVolumes(){
        volumeIndex+=1;
        volumeIndex=volumeIndex%profilesCameras.Length;
        volumeCameras.profile=profilesCameras[volumeIndex];
        volumeColors.profile=profilesColors[volumeIndex];
    }

    public void Landed(){
        if(distanceCrossed>=minDistanceCrossed && timePassed>=minTimePassed){
            ChangeImage();
            ChangeVolumes();
        }
    }

    public void AddDistance(float distance){
        distanceCrossed+=distance;
    }
}
