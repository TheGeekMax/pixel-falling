using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : BlocInterface{

    public override void ResetSave(){
        base.ResetSave();
    }
    
    public override string GetName(){
        return "Mixer";
    }


    public override DataSand[] GetNextStateData(int x, int y, GameObject[,] plateau){
        if(GetCell(x-1,y,plateau) != null && GetCell(x-1,y,plateau).GetComponent<BlocInterface>().GetName() == "Sand" &&
           GetCell(x+1,y,plateau) != null && GetCell(x+1,y,plateau).GetComponent<BlocInterface>().GetName() == "Sand"){
            SandObject currentObject = GetCell(x-1,y,plateau).GetComponent<SandObject>();
            SandObject currentObject2 = GetCell(x+1,y,plateau).GetComponent<SandObject>();
            //on creer un nouveau sable
            GameObject newSand = Instantiate(BlocManager.instance.GetBloc("sand"), Vector3.zero,Quaternion.identity);
            newSand.GetComponent<SandObject>().Visit();
            newSand.GetComponent<SandObject>().SetMoveState(true);
            //on met la couleur du sable a la somme (avec un max de 255) des deux sables
            newSand.GetComponent<SandObject>().color = new Vector3Int(Mathf.Min(currentObject.color.x + currentObject2.color.x,255),Mathf.Min(currentObject.color.y + currentObject2.color.y,255),Mathf.Min(currentObject.color.z + currentObject2.color.z,255));
            return new DataSand[]{new SandCreate(new Vector2Int(x,y), new Vector2Int(x,y-1),newSand),
                                  new SandRemove(new Vector2Int(x-1,y)),
                                  new SandRemove(new Vector2Int(x+1,y))};
        }
        return null;
    }
}
