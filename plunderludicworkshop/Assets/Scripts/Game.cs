using System.Collections;
using System.Collections.Generic;
using BizHawk.Common;
using Soil;
using UnityEngine;
using UnityHawk;

public class Game : MonoBehaviour
{

    public Emulator[] emulators;
    public GenericInputProvider[] inputProviders;
    public bool[] activationState;
    public int emulatorsActivatedNumber=0;
    public GameObject loadingText;
    public int currentEmulator=0;
    // Start is called before the first frame update
    void Start()
    {
        activationState=new bool[emulators.Length];
        inputProviders=new GenericInputProvider[emulators.Length];
        for(int i=0;i<emulators.Length;i++){
            activationState[i]=false;
            inputProviders[i]=emulators[i].GetComponent<GenericInputProvider>();
        }

        emulators[0].RegisterLuaCallback("LuaCallback",OnLuaCallBack);
    }

    string OnLuaCallBack(string arg){
        Debug.Log("hello frum unity");
        Debug.Log(arg);
        int n=int.Parse(arg);
        Debug.Log(n);
        return "";
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<activationState.Length;i++){
            if(activationState[i]==false){
                if(emulators[i].IsRunning){
                    //emulators[i].Pause();
                    //emulators[i].gameObject.SetActive(false);
                    emulators[i].GetComponent<MeshRenderer>().enabled=false;
                    emulatorsActivatedNumber++;
                    activationState[i]=true;
                    //inputProviders[i].key
                    if(emulatorsActivatedNumber>=emulators.Length){
                        //emulators[currentEmulator].gameObject.SetActive(true);
                        //emulators[currentEmulator].Unpause();
                        emulators[currentEmulator].GetComponent<MeshRenderer>().enabled=true;
                    }
                }
            }
        }
        if(emulatorsActivatedNumber>=emulators.Length){
            loadingText.SetActive(false);
            if(Input.GetKeyDown(KeyCode.Tab)){
                Switch();
            }else if(Input.GetKeyDown(KeyCode.A)){
                StartCoroutine("waitAndSwitch");
            }
        }

    }

    void Switch(){
        emulators[currentEmulator].GetComponent<MeshRenderer>().enabled=false;
        currentEmulator++;
        currentEmulator=currentEmulator%emulators.Length;
        // emulators[currentEmulator].gameObject.SetActive(true);
        // emulators[currentEmulator].Unpause();
        emulators[currentEmulator].GetComponent<MeshRenderer>().enabled=true;
    }

    IEnumerator waitAndSwitch(){
        yield return new WaitForSeconds(1);
        Switch();
        // IS THERE A WAY TO TURN OFF INPUT?
    }
}
