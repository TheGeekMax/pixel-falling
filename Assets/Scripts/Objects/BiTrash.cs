using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiTrash : BlocInterface{
    public override string GetName(){
        return "Bi-trash";
    }


    public override DataSand[] GetNextStateData(int x, int y, GameObject[,] plateau){
        Visit();
        if(GetCell(x,y+1,plateau) != null && GetCell(x,y+1,plateau).GetComponent<BlocInterface>().GetName() == "Sand" &&
           GetCell(x,y+2,plateau) != null && GetCell(x,y+2,plateau).GetComponent<BlocInterface>().GetName() == "Sand" &&
           GetCell(x,y+1,plateau).GetComponent<SandObject>().IsMoving() == false &&
           GetCell(x,y+2,plateau).GetComponent<SandObject>().IsMoving() == false){
            return new DataSand[]{
                new SandRemove(new Vector2Int(x,y+1)),
                new SandRemove(new Vector2Int(x,y+2)),
            };
        }
        return null;
    }
}
