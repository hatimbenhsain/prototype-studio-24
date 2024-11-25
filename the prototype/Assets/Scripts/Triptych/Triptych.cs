using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Triptych : MonoBehaviour
{
    public string nextSceneName;

    private bool loadingScene=false;
    public float loadTime=2f;
    private float loadTimer=0f;
    private string sceneToLoad="";



    public Image image;

    private int checkpointCounter=0;
    private int checkpointNumber=0;
    void Start()
    {
        image.gameObject.SetActive(false);
        checkpointNumber=FindObjectsOfType<Checkpoint>().Length;
    }

    void Update()
    {

        BeachClothParent[] bcps=FindObjectsOfType<BeachClothParent>();

        if(!loadingScene && bcps.Length>=1000){
            LoadScene(nextSceneName);
        }

        if(loadingScene){
            loadTimer+=Time.deltaTime;
            if(loadTimer>=loadTime){
                loadingScene=false;
                loadTimer=0f;
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    void LoadScene(string sceneName){
        loadingScene=true;
        sceneToLoad=sceneName;
        image.gameObject.SetActive(true);
    }

    public void Checkpoint(){
        checkpointCounter++;
        if(checkpointCounter>=checkpointNumber){
            StartCoroutine("GoNext",10f);
        }
    }

    IEnumerator GoNext(float seconds){
        yield return new WaitForSeconds(seconds);
        LoadScene(nextSceneName);
    }

}
