using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class BeachClothParent : MonoBehaviour
{
    private float timer;
    public float time=20f;
    public int clothesToGenerate=1;
    public GameObject prefab;
    public float maxDistance=5f;
    public int num=0;
    void Start()
    {
        time=time+Random.Range(-5f,5f);
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        if(timer>=time && timer-Time.deltaTime<time){
            for(int i=0;i<clothesToGenerate;i++){
                GameObject g=Instantiate(prefab);
                g.transform.position=new Vector3(Random.Range(-maxDistance,maxDistance)+transform.position.x,transform.position.y,transform.position.z);
                BeachClothParent bcp=g.GetComponent<BeachClothParent>();
                bcp.clothesToGenerate=clothesToGenerate+1;
                bcp.clothesToGenerate=Mathf.Min(bcp.clothesToGenerate,3);
            }
        }
    }
}
