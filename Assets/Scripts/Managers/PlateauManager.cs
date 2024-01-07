using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateauManager : MonoBehaviour{
    //attributes for init 
    private bool initialized = false;
    public static PlateauManager instance;

    private GameObject[,] plateau;
    private bool[,] placeable;
    private WinBloc[,] winGrid;

    public int width = 4;
    public int height = 4;

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
        //on recupère les infos de code manager
        width = CodeManager.instance.GetWidth();
        height = CodeManager.instance.GetHeight();

        plateau = new GameObject[width,height];
        placeable = new bool[width,height];
        winGrid = new WinBloc[width,height];

        for(int i = 0; i < width; i ++){
            for(int j = 0; j < height; j ++){
                int blocId = CodeManager.instance.GetBlocId(new Vector2Int(i,j));

                if(blocId != -1){
                    ForcedAddBloc(i,j,BlocManager.instance.GetBloc(blocId));
                }else{
                    plateau[i,j] = null;
                }

                placeable[i,j] = CodeManager.instance.IsLocked(new Vector2Int(i,j));
                
                int winBlocId = CodeManager.instance.GetWinId(new Vector2Int(i,j));

                if(winBlocId != -1){
                    WinManager.instance.AddWinBloc(new Vector2Int(i,j),WinManager.instance.wintile);
                }else{
                    winGrid[i,j] = null;
                }

            }
        }
        initialized = true;
    }

    //main function that do 1 itteration of the cellular automata
    public void Step(){
        for(int i = 0; i < width; i ++){
            for(int j = 0; j < height; j ++){
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
            for(int j = 0; j < height; j ++){
                if(plateau[i,j] != null){
                    plateau[i,j].GetComponent<BlocInterface>().ResetMoveState();
                    plateau[i,j].GetComponent<BlocInterface>().Reset();
                }
            }
        }

        //check win
        if(WinManager.instance.IsWin(plateau)){
            WinManager.instance.Win();
        }
    }

    public void SaveState(){
        ReplayManager.instance.SaveState(plateau);
    }

    public void LoadState(){
        ReplayManager.instance.LoadState(plateau);
    }

    public GameObject AddBloc(int x, int y,GameObject bloc){
        if(x < 0 || x >= width || y < 0 || y >= height) return null;
        if(plateau[x,y] != null){
            plateau[x,y].GetComponent<BlocInterface>().Rotate();
        }else{
            plateau[x,y] = Instantiate(bloc,new Vector3(x,y,0),Quaternion.identity) as GameObject;
            plateau[x,y].GetComponent<BlocInterface>().SetTargetPosition(new Vector2Int(x,y));
        }
        return plateau[x,y];
    }

    public GameObject ForcedAddBloc(int x, int y,GameObject bloc){
        if(x < 0 || x >= width || y < 0 || y >= height) return null;
        if(plateau[x,y] != null){
            Destroy(plateau[x,y]);
        }
        plateau[x,y] = Instantiate(bloc,new Vector3(x,y,0),Quaternion.identity) as GameObject;
        plateau[x,y].GetComponent<BlocInterface>().SetTargetPosition(new Vector2Int(x,y));
        return plateau[x,y];
    }

    public GameObject AddBlocWithoutInstantiate(Vector2Int cur, Vector2Int target,GameObject bloc){
        if(target.x < 0 || target.x >= width || target.y < 0 || target.y >= height || plateau[target.x,target.y] != null) return null;
        plateau[target.x,target.y] = bloc;
        plateau[target.x,target.y].transform.position = new Vector3(cur.x,cur.y,0);
        plateau[target.x,target.y].GetComponent<BlocInterface>().SetTargetPosition(target);
        return plateau[target.x,target.y];
    }

    public GameObject GetBloc(int x, int y){
        if(x < 0 || x >= width || y < 0 || y >= height || plateau[x,y] == null) return null;
        return plateau[x,y];
    }

    public int GetBlocId(int x, int y){
        if(x < 0 || x >= width || y < 0 || y >= height || plateau[x,y] == null) return -1;
        return plateau[x,y].GetComponent<BlocInterface>().GetId();
    }

    public void RemoveBloc(int x, int y){
        if(x < 0 || x >= width || y < 0 || y >= height || plateau[x,y] == null) return;
        ReplayManager.instance.RemoveBloc(new Vector2Int(x,y),plateau);
    }

    //fonctions for placeable piece
    public void SetPlaceable(Vector2Int pos, bool state){
        if(pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= height) return;
        placeable[pos.x,pos.y] = state;
        BackgroundManager.instance.UpdateTile(pos);
    }

    public void TogglePlaceable(Vector2Int pos){
        if(pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= height) return;
        placeable[pos.x,pos.y] = !placeable[pos.x,pos.y];
        BackgroundManager.instance.UpdateTile(pos);
    }

    public bool IsPlaceable(Vector2Int pos){
        if(pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= height) return false;
        return placeable[pos.x,pos.y];
    }

    //fonctions for win grid
    public void SetWinBloc(Vector2Int pos, WinBloc bloc){
        if(pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= height) return;
        winGrid[pos.x,pos.y] = bloc;
    }

    public WinBloc GetWinBloc(Vector2Int pos){
        if(pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= height) return null;
        return winGrid[pos.x,pos.y];
    }
}
