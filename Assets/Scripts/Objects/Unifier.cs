using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unifier : BlocInterface{

    public Vector3Int color;
    //fonctions relative au bloc
    public override string GetName(){
        return "Unifier";
    }


    public override DataSand[] GetNextStateData(int x, int y, GameObject[,] plateau){
        Visit();
        if(indexRotation == 0){
            if(GetCell(x-1,y,plateau) != null){
                BlocInterface bloc = GetCell(x-1,y,plateau).GetComponent<BlocInterface>();
                if(bloc.GetName() == "Sand"){
                    SandObject sand = GetCell(x-1,y,plateau).GetComponent<SandObject>();
                    Visit();
                    sand.color = this.color;
                }
            }
        }else{
            //right
            if(GetCell(x+1,y,plateau) != null){
                BlocInterface bloc = GetCell(x+1,y,plateau).GetComponent<BlocInterface>();
                if(bloc.GetName() == "Sand"){
                    SandObject sand = GetCell(x+1,y,plateau).GetComponent<SandObject>();
                    Visit();
                    sand.color = this.color;
                }
            }
        }
        return null;
    }
}
