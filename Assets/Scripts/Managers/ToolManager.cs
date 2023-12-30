using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolManager : MonoBehaviour{
    private bool initialized = false;

    public static ToolManager instance;


    //variables temporaire
    public TextMeshProUGUI blocText;


    public ButtonSlide tool; //0 - Place, 1 - remove 2- play
    public int currentBloc = 0;

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
    } 

    public void UseTool(Vector2Int coors){
        switch(tool.index){
            case 0:
                PlateauManager.instance.AddBloc(coors.x,coors.y,BlocManager.instance.GetBloc(currentBloc));
                break;
            case 1:
                PlateauManager.instance.RemoveBloc(coors.x,coors.y);
                break;
        }
    }

    public void ChangeBloc(int newBloc){
        currentBloc += newBloc;
        //on remet dans les bornes
        currentBloc = currentBloc%BlocManager.instance.GetLength();
        //change text
        blocText.text = BlocManager.instance.GetBloc(currentBloc).name;
    }

    void Update(){
        if(!initialized) return;
    }

    public void Initialize(){
        ChangeBloc(0);
        initialized = true;
    }
}
