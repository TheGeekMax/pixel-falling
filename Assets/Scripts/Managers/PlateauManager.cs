using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateauManager : MonoBehaviour{
    //attributes for init 
    private bool initialized = false;
    public static PlateauManager instance;

    private GameObject[,] plateau;
    private bool[,] placeable;

    public int width = 4;

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
        plateau = new GameObject[width,width];
        placeable = new bool[width,width];

        for(int i = 0; i < width; i ++){
            for(int j = 0; j < width; j ++){
                plateau[i,j] = null;
                placeable[i,j] = true;
            }
        }
        initialized = true;
    }

    //main function that do 1 itteration of the cellular automata
    public void Step(){
        for(int i = 0; i < width; i ++){
            for(int j = 0; j < width; j ++){
                if(plateau[i,j] != null && plateau[i,j].GetComponent<BlocInterface>().IsVisited() == false){
                    DataSand[] data = plateau[i,j].GetComponent<BlocInterface>().GetNextStateData(i,j,plateau);
                    if(data != null){
                        foreach(DataSand d in data){
                            if(d.ToCreate()){
                                AddBlocWithoutInstantiate(d.oldCoord,d.newCoord,d.GetBloc());
                            }else if(d.ToDestroy()){
                                RemoveBloc(d.oldCoord.x,d.oldCoord.y);
                            }else{ //on move
                                plateau[d.newCoord.x,d.newCoord.y] = plateau[d.oldCoord.x,d.oldCoord.y];
                                plateau[d.oldCoord.x,d.oldCoord.y] = null;
                                plateau[d.newCoord.x,d.newCoord.y].GetComponent<BlocInterface>().SetTargetPosition(d.newCoord);
                            }
                        }
                    }
                }
            }
        }

        for(int i = 0; i < width; i ++){
            for(int j = 0; j < width; j ++){
                if(plateau[i,j] != null){
                    plateau[i,j].GetComponent<BlocInterface>().ResetMoveState();
                    plateau[i,j].GetComponent<BlocInterface>().Reset();
                }
            }
        }
    }

    public void SaveState(){
        ReplayManager.instance.SaveState(plateau);
    }

    public void LoadState(){
        ReplayManager.instance.LoadState(plateau);
    }

    public GameObject AddBloc(int x, int y,GameObject bloc){
        if(x < 0 || x >= width || y < 0 || y >= width) return null;
        if(plateau[x,y] != null){
            plateau[x,y].GetComponent<BlocInterface>().Rotate();
        }else{
            plateau[x,y] = Instantiate(bloc,new Vector3(x,y,0),Quaternion.identity) as GameObject;
            plateau[x,y].GetComponent<BlocInterface>().SetTargetPosition(new Vector2Int(x,y));
        }
        return plateau[x,y];
    }

    public GameObject AddBlocWithoutInstantiate(Vector2Int cur, Vector2Int target,GameObject bloc){
        if(target.x < 0 || target.x >= width || target.y < 0 || target.y >= width || plateau[target.x,target.y] != null) return null;
        plateau[target.x,target.y] = bloc;
        plateau[target.x,target.y].transform.position = new Vector3(cur.x,cur.y,0);
        plateau[target.x,target.y].GetComponent<BlocInterface>().SetTargetPosition(target);
        return plateau[target.x,target.y];
    }

    public GameObject GetBloc(int x, int y){
        if(x < 0 || x >= width || y < 0 || y >= width || plateau[x,y] == null) return null;
        return plateau[x,y];
    }

    public void RemoveBloc(int x, int y){
        if(x < 0 || x >= width || y < 0 || y >= width || plateau[x,y] == null) return;
        ReplayManager.instance.RemoveBloc(new Vector2Int(x,y),plateau);
    }

    //fonctions for placeable piece
    public void SetPlaceable(Vector2Int pos, bool state){
        if(pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= width) return;
        placeable[pos.x,pos.y] = state;
        BackgroundManager.instance.UpdateTile(pos);
    }

    public void TogglePlaceable(Vector2Int pos){
        if(pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= width) return;
        placeable[pos.x,pos.y] = !placeable[pos.x,pos.y];
        BackgroundManager.instance.UpdateTile(pos);
    }

    public bool IsPlaceable(Vector2Int pos){
        if(pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= width) return false;
        return placeable[pos.x,pos.y];
    }
}
