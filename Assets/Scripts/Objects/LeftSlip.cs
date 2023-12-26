using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSlip : BlocInterface{
    //fonctions relative au bloc
    public override string GetName(){
        return "Slipper-Left";
    }


    public override DataSand[] GetNextStateData(int x, int y, GameObject[,] plateau){
        Visit();
        if(getCell(x,y+1,plateau) != null){
            BlocInterface bloc = getCell(x,y+1,plateau).GetComponent<BlocInterface>();
            if(bloc.GetName() == "Sand" && !bloc.IsMoving() &&
               getCell(x-1,y,plateau) == null && IsInBounds(x-1,y)){
                Visit();
                bloc.Visit();
                bloc.SetMoveState(true);
                return new DataSand[]{new SandMove(new Vector2Int(x,y+1),new Vector2Int(x-1,y))};
            }
        }
        return null;
    }
}
