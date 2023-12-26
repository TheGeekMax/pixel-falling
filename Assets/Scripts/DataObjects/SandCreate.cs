using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandCreate : DataSand{
    private GameObject toInstantiate;

    public override GameObject GetBloc(){
        return toInstantiate;
    }

    public override bool ToCreate(){
        return true;
    }

    public SandCreate(Vector2Int newCoord, GameObject toInstantiate){
        this.toInstantiate = toInstantiate;
        this.newCoord = newCoord;
    }
}
