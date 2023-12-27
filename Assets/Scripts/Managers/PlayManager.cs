using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayManager : MonoBehaviour{
    private bool initialized = false;
    public static PlayManager instance;

    public TextMeshProUGUI playText;


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
    }

    public void Restart(){
        //on restart la scene par id
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Play(){
        if(!GameManager.instance.started){
            GameManager.instance.started = true;
            playText.text = "Pause";
        }else{
            GameManager.instance.started = false;
            playText.text = "Play";
        }
    }

}
