using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour{
    private bool initialized = false;
    public static ReplayManager instance;

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

    //fonctions relatif au replay

    List<GameObject> removed = new List<GameObject>();

    public void SaveState(GameObject[,] plateau){
        //on parcours tout le plateau
        for(int i = 0; i < PlateauManager.instance.width; i++){
            for(int j = 0; j < PlateauManager.instance.width; j++){
                if(plateau[i,j] != null){
                    //on sauvegarde le bloc
                    plateau[i,j].GetComponent<BlocInterface>().Save(new Vector2Int(i,j));
                }
            }
        }
    }

    public void RemoveBloc(Vector2Int coors, GameObject[,] plateau){
        if(plateau[coors.x,coors.y] != null && plateau[coors.x,coors.y].GetComponent<BlocInterface>().GetSave()){
            removed.Add(plateau[coors.x,coors.y]);
        }
        plateau[coors.x,coors.y].GetComponent<BlocInterface>().Disapear();
        plateau[coors.x,coors.y] = null;
    }

    public void LoadState(GameObject[,] plateau){
        GameObject[,] newPlateau = new GameObject[PlateauManager.instance.width,PlateauManager.instance.width];
        //on parcours tout
        for(int i = 0; i < PlateauManager.instance.width; i++){
            for(int j = 0; j < PlateauManager.instance.width; j++){
                if(plateau[i,j] != null){
                    //on remet le bloc a sa place
                    if(plateau[i,j].GetComponent<BlocInterface>().GetSave()){
                        plateau[i,j].GetComponent<BlocInterface>().ResetSave();
                        newPlateau[plateau[i,j].GetComponent<BlocInterface>().saveCoors.x,plateau[i,j].GetComponent<BlocInterface>().saveCoors.y] = plateau[i,j];
                    }else{
                        //on le detruit
                        Destroy(plateau[i,j]);
                        plateau[i,j] = null;
                    }
                }
            }
        }
        //on remet les blocs supprimés
        foreach(GameObject bloc in removed){
            newPlateau[bloc.GetComponent<BlocInterface>().saveCoors.x,bloc.GetComponent<BlocInterface>().saveCoors.y] = bloc;
            bloc.SetActive(true);
            //on remet le bloc a sa place
            bloc.GetComponent<BlocInterface>().ResetSave();
        }
        removed.Clear();

        //on met a jour le plateau
        for(int i = 0; i < PlateauManager.instance.width; i++){
            for(int j = 0; j < PlateauManager.instance.width; j++){
                plateau[i,j] = newPlateau[i,j];
            }
        }
    }
}
