using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoScript : MonoBehaviour
{
    private DominoGame game;
    void Start()
    {
        game=FindObjectOfType<DominoGame>();   
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="Domino"){
            game.Explosion(other.GetContact(0).point);
        }
    }

    
}
