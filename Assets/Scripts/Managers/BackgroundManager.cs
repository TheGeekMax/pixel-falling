using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour{
    private bool initialized = false;

    public static BackgroundManager instance;

    [Header("Locked")]
    public Tilemap lockedMap;

    public Tile tile_on;
    public Tile tile_off;

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
        //on se base Plateaumanager
        for(int i = 0; i < PlateauManager.instance.width; i ++){
            for(int j = 0; j < PlateauManager.instance.width; j ++){
                if(PlateauManager.instance.IsPlaceable(new Vector2Int(i,j))){
                    lockedMap.SetTile(new Vector3Int(i-1,j-1,0),tile_on);
                }else{
                    lockedMap.SetTile(new Vector3Int(i-1,j-1,0),tile_off);
                }
            }
        }
        initialized = true;
    }


    public void UpdateTile(Vector2Int coors){
        if(PlateauManager.instance.IsPlaceable(coors)){
            lockedMap.SetTile(new Vector3Int(coors.x-1,coors.y-1,0),tile_on);
        }else{
            lockedMap.SetTile(new Vector3Int(coors.x-1,coors.y-1,0),tile_off);
        }
    }
}