using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandMove : DataSand{
    public SandMove(Vector2Int oldCoord, Vector2Int newCoord){
        this.oldCoord = oldCoord;
        this.newCoord = newCoord;
    }
}
