using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour{
    private bool initialized = false;

    public static TouchManager instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
    }

    void Update(){
        if(!initialized) return;

        if(GameManager.instance.started) return;

        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began){
                //recupere la coordonnee du touch
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;
                //transforme en coordonnee de plateau
                Vector2Int plateauCoord = new Vector2Int(Mathf.FloorToInt(touchPosition.x+0.5f),Mathf.FloorToInt(touchPosition.y+0.5f));
                ToolManager.instance.UseTool(plateauCoord);
            }
        }
    }

    public void Initialize(){
        initialized = true;
    }
}
