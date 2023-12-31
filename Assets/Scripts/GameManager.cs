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
        if(KeepElement.instance == null) UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        SandBoxManager.instance.Initialize();
        BlocManager.instance.Initialize();

        CodeManager.instance.Initialize();

        PlateauManager.instance.Initialize();
        BackgroundManager.instance.Initialize();
        PlayManager.instance.Initialize();
        ToolManager.instance.Initialize();
        TouchManager.instance.Initialize();
        ReplayManager.instance.Initialize();
        InventoryManager.instance.Initialize();
        WinManager.instance.Initialize();
    }

    // Update is called once per frame
    void FixedUpdate(){
        if(!started){
            CodeManager.instance.Encode();
            return;
        }
        
        if(step ++ > 10){
            step = 0;
            PlateauManager.instance.Step();
        }
    }
}
