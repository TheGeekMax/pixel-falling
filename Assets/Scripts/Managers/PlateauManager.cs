using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateauManager : MonoBehaviour{
    //attributes for init 
    private bool initialized = false;
    public static PlateauManager instance;

    private GameObject[,] plateau;

    public int width = 4;

    //variable temporaire
    public GameObject sand;
    public GameObject leftSlip;
    public GameObject unifier;
    public GameObject splitter;
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

        for(int i = 0; i < width; i ++){
            for(int j = 0; j < width; j ++){
                plateau[i,j] = null;
            }
        }

        AddBloc(2,7,sand);
        AddBloc(2,5,splitter);
        AddBloc(1,3,unifier);

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
                                AddBlocWithoutInstantiate(d.newCoord.x,d.newCoord.y,d.GetBloc());
                            }else if(d.ToDestroy()){
                                Destroy(plateau[d.oldCoord.x,d.oldCoord.y]);
                                plateau[d.oldCoord.x,d.oldCoord.y] = null;
                            }else{ //on move
                                plateau[d.newCoord.x,d.newCoord.y] = plateau[d.oldCoord.x,d.oldCoord.y];
                                plateau[d.oldCoord.x,d.oldCoord.y] = null;
                                plateau[d.newCoord.x,d.newCoord.y].transform.position = new Vector3(d.newCoord.x,d.newCoord.y,0);
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

    public GameObject AddBloc(int x, int y,GameObject bloc){
        if(x < 0 || x >= width || y < 0 || y >= width || plateau[x,y] != null) return null;
        plateau[x,y] = Instantiate(bloc,new Vector3(x,y,0),Quaternion.identity) as GameObject;
        return plateau[x,y];
    }

    public GameObject AddBlocWithoutInstantiate(int x, int y,GameObject bloc){
        if(x < 0 || x >= width || y < 0 || y >= width || plateau[x,y] != null) return null;
        plateau[x,y] = bloc;
        plateau[x,y].transform.position = new Vector3(x,y,0);
        return plateau[x,y];
    }


    //ondrawgizmos
    void OnDrawGizmos(){
        for(int i = 0; i < width; i ++){
            for(int j = 0; j < width; j ++){
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(new Vector3(i,j,0),new Vector3(1,1,0));
            }
        }
    }
}
