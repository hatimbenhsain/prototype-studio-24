using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGenerator : MonoBehaviour
{
    bool generatedSprites=false;
    public int number=20;
    public int maxTries=100;

    public GameObject prefab;

    public float maxDistance=3f;
    private BoxCollider boxCollider;
    void Start()
    {
        boxCollider=GetComponent<BoxCollider>();
    }

    void Update()
    {
        if(!generatedSprites){
            generatedSprites=true;
            int spritesGenerated=0;
            int tries=0;
            LayerMask mask = LayerMask.GetMask("Wall");
            while(tries<maxTries && spritesGenerated<number){
                Vector3 posDif=new Vector3(Random.Range(-maxDistance,maxDistance),0f,Random.Range(-maxDistance,maxDistance));
                Vector3 pos=transform.position+posDif;
                RaycastHit hit;
                //if(Physics.BoxCast(boxCollider.bounds.center+posDif,boxCollider.size*transform.localScale.x,Vector3.down,out hit,Quaternion.identity,1f,mask,QueryTriggerInteraction.Collide)){
                Debug.DrawRay(pos,Vector3.down,Color.magenta,5f);
                if(!Physics.Raycast(pos,Vector3.down,out hit,1f,mask) &&
                    Physics.Raycast(pos,Vector3.down,out hit,2f,mask)){
                    GameObject g=Instantiate(prefab);
                    g.transform.position=pos;
                    spritesGenerated++;
                    g.GetComponent<SpriteGenerator>().enabled=false;
                }
                tries++;
            }
        }
    }

}
