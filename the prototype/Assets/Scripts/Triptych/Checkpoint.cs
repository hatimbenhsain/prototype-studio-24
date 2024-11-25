using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private bool check=false;

    private void OnTriggerEnter(Collider other) {
        if(!check && other.tag=="Player"){
            Debug.Log("Checkpoint");
            FindObjectOfType<Triptych>().Checkpoint();
            check=true;
        }
    }
}
