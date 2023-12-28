using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandObject : BlocInterface{
    public Vector3Int color;

    public void Update(){
        base.Update();
        UpdateColor();
    }

    public void UpdateColor(){
        GetComponent<SpriteRenderer>().color = new Color32((byte)color.x,(byte)color.y,(byte)color.z,255);
    }
    //fonctions relative au bloc
    public override string GetName(){
        return "Sand";
    }


    public override DataSand[] GetNextStateData(int x, int y, GameObject[,] plateau){
        Visit();
        if(GetCell(x,y-1,plateau) == null && IsInBounds(x,y-1)){
            SetMoveState(true);
            return new DataSand[]{new SandMove(new Vector2Int(x,y),new Vector2Int(x,y-1))};
        }
        return null;
    }
}