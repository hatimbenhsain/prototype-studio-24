using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHawk;

public class DGame : MonoBehaviour
{
    public Emulator dEmulator;
    public Savestate dSaveState;
    public Emulator sonicEmulator;
    public Savestate sonicSaveState;

    public GameObject loadingScreen;

    public Savestate[] saveStates;
    public Savestate puzzleSaveState;
    public int saveStateIndex=0;

    public bool hasLoaded=false;
    void Start()
    {
        Shuffle(saveStates);
        dEmulator.LoadState(saveStates[saveStateIndex]);
        sonicEmulator.LoadState(sonicSaveState);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            SwitchSaveState(dEmulator);
        }
        if(Input.GetKeyDown(KeyCode.Backspace)){
            sonicEmulator.LoadState(sonicSaveState);
        }

        if(dEmulator.Status==Emulator.EmulatorStatus.Started && sonicEmulator.Status==Emulator.EmulatorStatus.Started){
            loadingScreen.SetActive(true);
        }else if(dEmulator.Status==Emulator.EmulatorStatus.Running && sonicEmulator.Status==Emulator.EmulatorStatus.Running){
            loadingScreen.SetActive(false);
            if(!hasLoaded){
                hasLoaded=true;
            }
            dEmulator.LoadState(saveStates[saveStateIndex]);
        }
    }

    public void SwitchSaveState(Emulator emulator){
        saveStateIndex+=1;
        saveStateIndex=saveStateIndex%saveStates.Length;
        emulator.LoadState(saveStates[saveStateIndex]);
    }
    void Shuffle(Savestate[] array)
    {
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = Random.Range(0, n);
            Savestate t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }
}
