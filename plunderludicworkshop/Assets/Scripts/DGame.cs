using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public GameObject camera;

    public bool cameraInField=false;

    public float minAngle=20f;
    public float maxAngle=30f;

    public bool verticalZone=true;
    public bool switchZone=false;

    public float switchWaitTime=1f;

    public Light light;
    public float lightMin=2.5f;
    public float lightMax=5f;
    public float lightPeriod=10f;
    public float lightTimer=0f;

    void Start()
    {
        Shuffle(saveStates);
        dEmulator.LoadState(saveStates[saveStateIndex]);
        sonicEmulator.LoadState(sonicSaveState);
    }

    void Update()
    {
        lightTimer+=Time.deltaTime;
        lightTimer=lightTimer%(2*lightPeriod);
        light.intensity=Mathf.Sin(Mathf.PI*lightTimer/lightPeriod)*(lightMax-lightMin)+lightMin;

        // if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Return)){
        //     SwitchSaveState(dEmulator);
        // }
        // if(Input.GetKeyDown(KeyCode.Backspace)){
        //     sonicEmulator.LoadState(sonicSaveState);
        // }

        if(dEmulator.Status==Emulator.EmulatorStatus.Started && sonicEmulator.Status==Emulator.EmulatorStatus.Started){
            loadingScreen.SetActive(true);
        }else if(dEmulator.Status==Emulator.EmulatorStatus.Running && sonicEmulator.Status==Emulator.EmulatorStatus.Running){
            loadingScreen.SetActive(false);
            if(!hasLoaded){
                hasLoaded=true;
            }
        }

        Vector3 forward=Vector3.forward;

        if(!verticalZone){
            forward=Vector3.right;
        }

        if(!switchZone){
            float angle=Mathf.Min(Vector3.Angle(camera.transform.forward,forward),Vector3.Angle(camera.transform.forward,-forward));
            if(!cameraInField){
                if(angle>=maxAngle){
                    cameraInField=true;
                    SwitchSaveState(dEmulator);
                }
            }else{
                if(angle<=minAngle){
                    cameraInField=false;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene("DScene");
        }
    }

    public void SwitchSaveState(Emulator emulator){
        saveStateIndex+=1;
        saveStateIndex=saveStateIndex%saveStates.Length;
        emulator.LoadState(saveStates[saveStateIndex]);
        sonicEmulator.LoadState(sonicSaveState);
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

    public IEnumerator SwitchSaveStateCoroutine(){
        yield return new WaitForSeconds(switchWaitTime);
        SwitchSaveState(dEmulator);
    }
}
