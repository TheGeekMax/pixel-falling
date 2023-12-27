using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    //attributes for init
    public static GameManager instance;

    //variables temporaires
    private int step = 0;

    public bool started = false;

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
    }

    void Start(){
        PlateauManager.instance.Initialize();
        PlayManager.instance.Initialize();
    }

    // Update is called once per frame
    void FixedUpdate(){
        if(!started) return;
        
        if(step ++ > 10){
            step = 0;
            PlateauManager.instance.Step();
        }
    }
}
