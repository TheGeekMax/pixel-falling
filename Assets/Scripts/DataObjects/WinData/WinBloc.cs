using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WinBloc{
    public bool isColored = false;
    public Vector3 color;

    public WinBloc(bool isColored, Vector3 color){
        this.isColored = isColored;
        this.color = color;
    }
    public virtual bool IsColored(){
        return isColored;
    }

    public virtual Vector3 GetColor(){
        return color;
    }

    public bool IsWin(int x, int y, GameObject[,] plateau){
        if(plateau[x,y] == null) return false;
        if(plateau[x,y].GetComponent<BlocInterface>().GetName() != "Sand") return false;
        
        if(IsColored()){
            SandObject sand = plateau[x,y].GetComponent<SandObject>();
            if(sand.color == GetColor()){
                return true;
            }
            return false;
        }
        return true;
    }
    
}
