using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSlip : BlocInterface{
    //fonctions relative au bloc
    public override string GetName(){
        return "Slipper";
    }


    public override DataSand[] GetNextStateData(int x, int y, GameObject[,] plateau){
        Visit();
        if(GetCell(x,y+1,plateau) != null){
            BlocInterface bloc = GetCell(x,y+1,plateau).GetComponent<BlocInterface>();
            if(bloc.GetName() == "Sand" && !bloc.IsMoving()){
               
               switch(indexRotation){
                    case 0:
                        //left
                        if(GetCell(x-1,y,plateau) == null && IsInBounds(x-1,y)){
                            Visit();
                            bloc.Visit();
                            bloc.SetMoveState(true);
                            return new DataSand[]{new SandMove(new Vector2Int(x,y+1),new Vector2Int(x-1,y))};
                        }
                        break;
                    case 1:
                        //right
                        if(GetCell(x+1,y,plateau) == null && IsInBounds(x+1,y)){
                            Visit();
                            bloc.Visit();
                            bloc.SetMoveState(true);
                            return new DataSand[]{new SandMove(new Vector2Int(x,y+1),new Vector2Int(x+1,y))};
                        }
                        break;
               }
            }
        }
        return null;
    }
}
