using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSand{

    public Vector2Int oldCoord = new Vector2Int(0,0);
    public Vector2Int newCoord = new Vector2Int(0,0);

    public virtual bool ToDestroy(){
        return false;
    }

    public virtual bool ToCreate(){
        return false;
    }

    public virtual GameObject GetBloc(){
        return null;
    }
}
