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
                if(PlateauManager.instance.IsPlaceable(coors) || PlateauManager.instance.GetBloc(coors.x,coors.y) != null)
                    PlateauManager.instance.AddBloc(coors.x,coors.y,BlocManager.instance.GetBloc(currentBloc));
                break;
            case 1:
                if(PlateauManager.instance.IsPlaceable(coors))
                    PlateauManager.instance.RemoveBloc(coors.x,coors.y);
                break;
            case 2:
                PlateauManager.instance.TogglePlaceable(coors);
                break;
        }
    }

    public void ChangeBloc(int newBloc){
        currentBloc = newBloc;
        //change text
        blocText.text = BlocManager.instance.GetBlocData(currentBloc).name;
    }

    void Update(){
        if(!initialized) return;
    }

    public void Initialize(){
        ChangeBloc(0);
        initialized = true;
    }
}
