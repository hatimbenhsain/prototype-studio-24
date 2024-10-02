using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoScript : MonoBehaviour
{
    private DominoGame game;
    public DominoHand handFront;
    public DominoHand handBack;
    void Start()
    {
        game=FindObjectOfType<DominoGame>();   
        handFront.transform.parent=transform.parent;
        handBack.transform.parent=transform.parent;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="Domino"){
            game.Explosion(other.GetContact(0).point);
        }
    }

    public void Hovered(bool front){
        DominoHand hand;
        if(front){
            hand=handFront;
        }else{
            hand=handBack;
        }
        hand.gameObject.SetActive(true);
        hand.SetTransparent();
        gameObject.layer=3;
    }

    public void NotHovered(){
        handFront.gameObject.SetActive(false);
        handBack.gameObject.SetActive(false);
        gameObject.layer=0;
    }

    public void Push(bool front){
        DominoHand hand;
        if(front){
            hand=handFront;
        }else{
            hand=handBack;
        }
        hand.gameObject.SetActive(true);
        hand.Push();
    }
    
}
