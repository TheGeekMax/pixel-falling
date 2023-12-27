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
        if(GetCell(x,y+1,plateau) != null){
            BlocInterface bloc = GetCell(x,y+1,plateau).GetComponent<BlocInterface>();
            if(bloc.GetName() == "Sand" && !bloc.IsMoving() &&
               GetCell(x,y-1,plateau) == null && IsInBounds(x,y-1)){
                SandObject sand = GetCell(x,y+1,plateau).GetComponent<SandObject>();
                Visit();
                bloc.Visit();
                bloc.SetMoveState(true);
                sand.color = this.color;
                return new DataSand[]{new SandMove(new Vector2Int(x,y+1),new Vector2Int(x,y-1))};
            }
        }
        return null;
    }
}
