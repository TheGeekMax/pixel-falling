using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBoxManager : MonoBehaviour{
    private bool initialized = false;

    public static SandBoxManager instance;

    //donnes de la sandbox
    public bool isSandbox = false;
    public int maxBlockCount = 64;

    public GameObject[] toDisable;

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
    }

    void Update(){
        if(!initialized) return;
        if(!isSandbox){
            foreach(GameObject go in toDisable){
                go.SetActive(false);
            }
        }else{
            foreach(GameObject go in toDisable){
                go.SetActive(true);
            }
        }
    }

    public void Initialize(){
        isSandbox = KeepElement.instance.isSandbox;
        initialized = true;
    }
}
