using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : BlocInterface{
    public override void ResetSave(){
        base.ResetSave();
    }
    
    public override string GetName(){
        return "Wall";
    }


    public override DataSand[] GetNextStateData(int x, int y, GameObject[,] plateau){
        return null;
    }
}
