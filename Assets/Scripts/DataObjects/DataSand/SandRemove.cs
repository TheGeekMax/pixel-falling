using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandRemove : DataSand{
    public override bool ToDestroy(){
        return true;
    }

    public SandRemove(Vector2Int oldCoord){
        this.oldCoord = oldCoord;
    }
}
