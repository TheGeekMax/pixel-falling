using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepElement : MonoBehaviour{

    public static KeepElement instance;

    public string levelCode = "";

    public bool isSandbox = false;

    void Awake(){
        DontDestroyOnLoad(this.gameObject);
        if(instance == null){
            instance = this;
        }else{
            //on transfer les données au nouveaux

            //puis on détruit l'ancien
            Destroy(instance.gameObject);
            instance = this;
        }
    }

    public void SetLevelCode(string code){
        levelCode = code;
    }

    public void SetSandbox(bool value){
        isSandbox = value;
    }

    public void Play(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
