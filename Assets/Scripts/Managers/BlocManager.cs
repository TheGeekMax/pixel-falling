using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocManager : MonoBehaviour{
    private bool initialized = false;

    public static BlocManager instance;

    public BlocData[] blocs;

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
        for(int i = 0; i < blocs.Length; i++){
            blocs[i].prefab.GetComponent<BlocInterface>().SetId(i);
        }
        //TODO : dict of <int,BlocType> with int number of blocs of this type
        initialized = true;
    }
    

    //fonctions relatives a la recherche de blocs
    public BlocData GetBlocData(string name){
        foreach(BlocData b in blocs){
            if(b.id == name){
                return b;
            }
        }
        return null;
    }

    public BlocData GetBlocData(int index){
        return blocs[index];
    }

    public GameObject GetBloc(int index){
        return blocs[index].prefab;
    }

    public GameObject GetBloc(string name){
        return GetBlocData(name).prefab;
    }

    public int GetLength(){
        return blocs.Length;
    }
}
