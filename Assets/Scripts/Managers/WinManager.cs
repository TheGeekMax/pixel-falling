using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour{
    public bool win = false;
    private bool initialized = false;
    public static WinManager instance;

    public WinTile wintile;

    [Header("temp")]
    public GameObject WinSprite;
    public Sprite WinSpriteSprite;
    public Sprite waitWinSprite;
    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
    }

    void Update(){
        if(!initialized) return;
    }

    public void Initialize(){
        initialized = true;
    }

    public void AddWinBloc(Vector2Int coord, WinTile bloc){
        BackgroundManager.instance.UpdateWinTile(coord,bloc.tile);
        WinBloc winBloc = new WinBloc(bloc.isColored,bloc.color);

        PlateauManager.instance.SetWinBloc(coord,winBloc);
    }

    public void RemoveWinBloc(Vector2Int coord){
        BackgroundManager.instance.UpdateWinTile(coord,null);
        PlateauManager.instance.SetWinBloc(coord,null);
    }

    public void UpdateWinBloc(Vector2Int coord){
        if(PlateauManager.instance.GetWinBloc(coord) != null){
            RemoveWinBloc(coord);
            return;
        }
        AddWinBloc(coord,wintile);

    }

    public bool IsWin(GameObject[,] plateau){
        int width = PlateauManager.instance.width;
        int height = PlateauManager.instance.height;
        for(int i = 0; i < width; i ++){
            for(int j = 0; j < height; j ++){
                WinBloc winBloc = PlateauManager.instance.GetWinBloc(new Vector2Int(i,j));
                if(winBloc != null && !winBloc.IsWin(i,j,plateau)){
                    return false;
                }
            }
        }
        return true;
    }

    public void Win(){
        if(win) return;
        win = true;
        WinSprite.GetComponent<Image>().sprite = WinSpriteSprite;
    }

    public void Reset(){
        win = false;
        WinSprite.GetComponent<Image>().sprite = waitWinSprite;
    }
}
