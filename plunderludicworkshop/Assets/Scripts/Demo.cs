using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityHawk;

public class Demo : MonoBehaviour
{
    public Emulator emulator;
    public Savestate saveState;

    public GameObject loadingScreen;

    public Savestate[] saveStates;
    public Savestate puzzleSaveState;
    public int saveStateIndex=0;

    private Animator animator;

    private string gamefaqText="B.l.u.e. (henceforth referred to as just \"BLUE\") is a 1998 puzzle adventure game for the Playstation. Like much media of its time, it's dripping with a certain romantic reverence for the ocean. Everything from the graphics to the music and pacing creates an experience that isn't just immersive, but borderline meditative. Of course, there are also elements of tension and even horror in this game, but the vast majority of it is spent in an underwater trance. Unfortunately, this game never made it to the West in its heyday but, thanks to the dedication of fan translators, you can now play it in English as of December 2022! This guide is based on that translation, but will be worded in such a way that you can still use it to play the untranslated version. This game is incredibly simple, in terms of mechanics, and can be played on any version of the Playstation controller. All menus are navigated using the D-PAD and CIRCLE to confirm, X to cancel. Fortunately, they are almost entirely in English, even in the Japanese version, so I won't spell out each option. You are a girl named Maia visiting your father Halbert, a researcher at Northern Cross Research Facility. Here, he is studying underwater ruins belonging to a lost civilization called Mu. You have been dreaming of these ruins and you're curious as to what your connection to them is. After some short dialog with your father and reuniting with your dolphin friend, Luka, you're off into the ocean. In the water, press SQUARE to accelerate. Tap it to go faster (your speed is shown by a thin bar in the top right) then hold to maintain that speed. Follow Luka to the North to encounter another diver who, unfortunately, is victim to a tutorial. Go into your AI menu (TRIANGLE > \"AI\") and select CO-OP MODE (2nd option from the left). Now, swim at full speed against the boulder. The man thanks you and says he's alright, somehow. NOW, LISTEN, as this part is [NECESSARY TO GET THE BEST ENDING]! Your partner AI's STANDBY MODE actually affects the ending of the game. Whenever you do a task that requires your partner to be on STANDBY, do it as QUICKLY AS POSSIBLE, then take them off STANDBY. Leaving your partner in STANDBY for over 3 minutes, in total, will lock you out of the best ending! Anyway: You'll be back at the base now, so exit the DIVING DECK. The map screen of the base is one of the few places you can SAVE your game (TRIANGLE > \"SAVE\") so I recommend doing that. For base navigation, I'll be providing a simple map of the middle and top floor. No, I'm not just being a tryhard lol. I'm doing this in case anyone is playing in Japanese (like original hardware purists). It's easier to show you which rooms you need to visit and in what order. A lot of the time, order doesn't matter, but I'm also focusing on efficiency, story flow and summarizing events. Anyway:";
    public float gamefaqScrollSpeed=1f;
    private string madonnaSong;
    public string[] madonnaArray;

    public float madonnaStartTime=52f;
    public float madonnaLength=373f;
    public float madonnaScrollSpeed=1f;

    public float timer=0f;

    public TMP_Text madonnaTMP;
    public TMP_Text gamefaqsTMP;

    public int gamefaqsTextLength=100;

    public RectTransform minimapPlayer;
    public float minimapMinX;
    public float minimapMaxX;
    public float minimapMinY;
    public float minimapMaxY;

    public float irlMinX;
    public float irlMaxX;
    public float irlMinY;
    public float irlMaxY;
    
    public Transform player;

    private bool hasLoaded=false;

    void Start()
    {
        Shuffle(saveStates);
        emulator.LoadState(saveStates[saveStateIndex]);
        animator=GetComponent<Animator>();
        animator.enabled=false;
        for(int i=0;i<gamefaqsTextLength;i++){        
            gamefaqText=" "+gamefaqText;
        }
        madonnaSong=madonnaTMP.text;
        madonnaTMP.text="";
        madonnaArray=madonnaSong.Split("\n");
        string[] stringArray=new string[11];
        for(int i=0;i<11;i++){        
            stringArray[i]="";
        }
        string[] newArray = new string[madonnaArray.Length + stringArray.Length];
        madonnaArray.CopyTo(newArray, 0);
        stringArray.CopyTo(newArray, madonnaArray.Length);
        madonnaArray=newArray;
    }

    void Update()
    {
        timer+=Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Return)){
            if(saveStates[saveStateIndex]==puzzleSaveState){
                SwitchSaveState();
            }else{
                emulator.LoadState(saveStates[saveStateIndex]);
            }
        }
        if(emulator.Status==Emulator.EmulatorStatus.Started){
            loadingScreen.SetActive(true);
        }else if(emulator.Status==Emulator.EmulatorStatus.Running){
            loadingScreen.SetActive(false);
            animator.enabled=true;
            if(!hasLoaded){
                hasLoaded=true;
                emulator.LoadState(saveStates[saveStateIndex]);
            }
        }
        if(Input.GetKeyDown(KeyCode.P)){
            emulator.LoadState(puzzleSaveState);
        }

        int gamefaqsIndex=Mathf.FloorToInt(timer*gamefaqScrollSpeed);
        gamefaqsIndex=gamefaqsIndex%gamefaqText.Length;
        gamefaqsTMP.text=gamefaqText.Substring(gamefaqsIndex,Mathf.Clamp(gamefaqsTextLength,0,gamefaqText.Length-gamefaqsIndex))+
        gamefaqText.Substring(0,Mathf.Clamp(gamefaqsTextLength-(gamefaqText.Length-gamefaqsIndex),0,gamefaqsTextLength));

        if(timer>=madonnaStartTime){
            string txt="";
            float k=madonnaArray.Length*(timer-madonnaStartTime)/madonnaLength;
            int l=Mathf.FloorToInt(k);
            for(int i=Mathf.Max(0,l-11);i<l;i++){
                string t=madonnaArray[i%madonnaArray.Length];
                if(i==l-1){
                    t=t.Substring(0,Mathf.Min(Mathf.FloorToInt((k-l)*madonnaScrollSpeed),t.Length));
                }
                txt=txt+t+"\n";
            }
            madonnaTMP.text=txt;
        }

        float x=Mathf.Clamp(transform.position.x,irlMinX,irlMaxX)-irlMinX;
        x=minimapMinX+x*(minimapMaxX-minimapMinX)/(irlMaxX-irlMinX);
        float y=Mathf.Clamp(transform.position.z,irlMinY,irlMaxY)-irlMinY;
        y=minimapMinY+y*(minimapMaxY-minimapMinY)/(irlMaxY-irlMinY);

        minimapPlayer.anchoredPosition=new Vector2(x,y);
        minimapPlayer.rotation=Quaternion.Euler(new Vector3(0f,0f,-transform.rotation.eulerAngles.y));
    }
    

    public void SwitchSaveState(){
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
