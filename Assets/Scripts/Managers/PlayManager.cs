using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour{
    private bool initialized = false;
    public static PlayManager instance;

    private bool playing = false;

    public ButtonSlide tool; //0 - Place, 1 - remove, 2- play

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
    }

    public void Initialize(){
        initialized = true;
    }

    void Update(){
        if(!initialized) return;

        if(tool.index == 2 && !playing){
            playing = true;
            Play(true);
        }else if(tool.index != 2 && playing){
            playing = false;
            Play(false);
        }
    }

    public void Restart(){
        //on restart la scene par id
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Play(bool play){
        GameManager.instance.started = !play;

        if(!GameManager.instance.started){
            GameManager.instance.started = true;
            PlateauManager.instance.SaveState();
        }else{
            GameManager.instance.started = false;
            PlateauManager.instance.LoadState();
        }
    }

}
