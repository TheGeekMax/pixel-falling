using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : BlocInterface{

    private int timer = 5;
    public override string GetName(){
        return "Generator";
    }

    public override void ResetSave(){
        base.ResetSave();
        timer = 5;
    }


    public override DataSand[] GetNextStateData(int x, int y, GameObject[,] plateau){
        if(timer > 0){
            timer--;
            return null;
        }
        if(GetCell(x,y-1,plateau) == null && IsInBounds(x,y-1)){
            timer = 5;
            GameObject newSand = Instantiate(BlocManager.instance.GetBloc("sand"), Vector3.zero,Quaternion.identity);
            newSand.GetComponent<SandObject>().Visit();
            newSand.GetComponent<SandObject>().SetMoveState(true);
            return new DataSand[]{new SandCreate(new Vector2Int(x,y), new Vector2Int(x,y-1),newSand)};
        }
        return null;
    }
}
