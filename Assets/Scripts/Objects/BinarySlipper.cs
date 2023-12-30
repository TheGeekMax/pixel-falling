using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinarySlipper : BlocInterface{
    public Sprite spriteLeft;
    public Sprite spriteRight;

    private int state = 0; //0 = left, 1 = right

    public override void ResetSave(){
        base.ResetSave();
        state = 0;
        GetComponent<SpriteRenderer>().sprite = spriteLeft;
    }
    
    public override string GetName(){
        return "Slipper-binary";
    }


    public override DataSand[] GetNextStateData(int x, int y, GameObject[,] plateau){
        Visit();
        if(GetCell(x,y+1,plateau) != null && GetCell(x,y+1,plateau).GetComponent<BlocInterface>().GetName() == "Sand" && GetCell(x,y+1,plateau).GetComponent<SandObject>().IsMoving() == false){
            SandObject currentObject = GetCell(x,y+1,plateau).GetComponent<SandObject>();
            if(state == 0){
                if(GetCell(x+1,y,plateau) != null || !IsInBounds(x+1,y))
                    return null;
                currentObject.Visit();
                currentObject.SetMoveState(true);
                state = 1;
                GetComponent<SpriteRenderer>().sprite = spriteRight;
                return new DataSand[]{new SandMove(new Vector2Int(x,y+1),new Vector2Int(x+1,y))};
            }else{
                if(GetCell(x-1,y,plateau) != null || !IsInBounds(x-1,y))
                    return null;
                currentObject.Visit();
                currentObject.SetMoveState(true);
                state = 0;
                GetComponent<SpriteRenderer>().sprite = spriteLeft;
                return new DataSand[]{new SandMove(new Vector2Int(x,y+1),new Vector2Int(x-1,y))};
            }
        }
        return null;
    }
}
