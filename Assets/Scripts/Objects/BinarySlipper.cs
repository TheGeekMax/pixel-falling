using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinarySlipper : BlocInterface{
    public override void ResetSave(){
        base.ResetSave();
    }
    
    public override string GetName(){
        return "Slipper-binary";
    }


    public override DataSand[] GetNextStateData(int x, int y, GameObject[,] plateau){
        Visit();
        if(GetCell(x,y+1,plateau) != null && GetCell(x,y+1,plateau).GetComponent<BlocInterface>().GetName() == "Sand" && GetCell(x,y+1,plateau).GetComponent<SandObject>().IsMoving() == false){
            SandObject currentObject = GetCell(x,y+1,plateau).GetComponent<SandObject>();
            if(indexRotation == 0){
                if(GetCell(x+1,y,plateau) != null || !IsInBounds(x+1,y))
                    return null;
                currentObject.Visit();
                currentObject.SetMoveState(true);
                Rotate();
                return new DataSand[]{new SandMove(new Vector2Int(x,y+1),new Vector2Int(x+1,y))};
            }else{
                if(GetCell(x-1,y,plateau) != null || !IsInBounds(x-1,y))
                    return null;
                currentObject.Visit();
                currentObject.SetMoveState(true);
                Rotate();
                return new DataSand[]{new SandMove(new Vector2Int(x,y+1),new Vector2Int(x-1,y))};
            }
        }
        return null;
    }
}
