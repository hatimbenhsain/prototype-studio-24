using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHawk;

public class Demo : MonoBehaviour
{
    public Emulator emulator;
    public Savestate saveState;

    public GameObject loadingScreen;


    void Start()
    {
        emulator.LoadState(emulator.saveStateFile);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            emulator.LoadState(emulator.saveStateFile);
        }
        if(emulator.Status==Emulator.EmulatorStatus.Started){
            loadingScreen.SetActive(true);
        }else if(emulator.Status==Emulator.EmulatorStatus.Running){
            loadingScreen.SetActive(false);
        }
    }
}
