using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : BlocInterface{
    //fonctions relative au bloc
    public override string GetName(){
        return "Spliter";
    }


    public override DataSand[] GetNextStateData(int x, int y, GameObject[,] plateau){
        Visit();

        if(GetCell(x,y+1,plateau) == null || GetCell(x,y+1,plateau).GetComponent<BlocInterface>().GetName() != "Sand"){
            return null;
        }

        bool leftFree = (GetCell(x+1,y,plateau) == null && IsInBounds(x+1,y));
        bool rightFree = (GetCell(x-1,y,plateau) == null && IsInBounds(x-1,y));
        SandObject currentObject = GetCell(x,y+1,plateau).GetComponent<SandObject>();

        if(leftFree && !rightFree){
            //left seulement
            currentObject.Visit();
            currentObject.SetMoveState(true);
            return new DataSand[]{new SandMove(new Vector2Int(x,y+1),new Vector2Int(x+1,y))};
        }else if(!leftFree && rightFree){
            //right seulement
            currentObject.Visit();
            currentObject.SetMoveState(true);
            return new DataSand[]{new SandMove(new Vector2Int(x,y+1),new Vector2Int(x-1,y))};
        }else if(leftFree && rightFree){
            //both
            currentObject.Visit();
            currentObject.SetMoveState(true);

            GameObject newSand = Instantiate(BlocManager.instance.GetBloc("sand"), Vector3.zero,Quaternion.identity);
            newSand.GetComponent<SandObject>().color = GetCell(x,y+1,plateau).GetComponent<SandObject>().color;
            newSand.GetComponent<SandObject>().Visit();
            newSand.GetComponent<SandObject>().SetMoveState(true);
            return new DataSand[]{
                new SandMove(new Vector2Int(x,y+1),new Vector2Int(x+1,y)),
                new SandCreate(new Vector2Int(x-1,y),newSand),
            };
        }
        return null;
    }
}
