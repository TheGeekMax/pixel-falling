using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolManager : MonoBehaviour{
    private bool initialized = false;

    public static ToolManager instance;


    //variables temporaire
    public TextMeshProUGUI toolText;
    public TextMeshProUGUI blocText;


    public int tool = 0; //0 - Place, 1 - Remove
    public int currentBloc = 0;

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
    } 

    public void UseTool(Vector2Int coors){
        switch(tool){
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

    public void ChangeTool(int newTool){
        tool += newTool;
        //on remet dans les bornes
        tool = tool%2;
        //change text
        if(tool == 0){
            toolText.text = "Place";
        }else{
            toolText.text = "Remove";
        }
    }

    void Update(){
        if(!initialized) return;
    }

    public void Initialize(){
        ChangeBloc(0);
        ChangeTool(0);
        initialized = true;
    }
}
