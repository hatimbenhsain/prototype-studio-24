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
    void Start()
    {
        image.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
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
}
