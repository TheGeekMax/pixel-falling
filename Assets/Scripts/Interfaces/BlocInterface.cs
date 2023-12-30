using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocInterface : MonoBehaviour {
    protected bool visited = false;
    protected bool moving = false;

    //fonctions pour eviter les repetitions
    public virtual void Visit(){
        visited = true;
    }

    public virtual void Reset(){
        visited = false;
    }

    public virtual bool IsVisited(){
        return visited;
    }

    //fonctions pour detecter les mouvements
    public virtual void SetMoveState(bool state){
        moving = state;
    }

    public virtual void ResetMoveState(){
        moving = false;
    }

    public virtual bool IsMoving(){
        return moving;
    }

    bool toSave = false;
    public Vector2Int saveCoors;
    bool isDisapearing = false;
    int resetCount = 0;
    public void Save(Vector2Int coors){
        toSave = true;
        saveCoors = coors;
    }

    public virtual void ResetSave(){
        toSave = false;
        isDisapearing = false;
        resetCount = 0;
        transform.localScale = Vector3.one;
        //on remet le bloc a sa place
        targetPosition = new Vector3(saveCoors.x,saveCoors.y,0);
    }

    public bool GetSave(){
        return toSave;
    }

    public void Disapear(){
        isDisapearing = true;
    }

    private void Shutdown(){
        if(toSave){
            //on le desactive
            gameObject.SetActive(false);
        }else{
            //on le detruit
            Destroy(gameObject);
        }
    }

    //fonctions pour les animations
    public Vector3 targetPosition;

    public void Update(){
        transform.position = Vector3.Lerp(transform.position,targetPosition,Time.deltaTime * 10);

        if(isDisapearing){
            resetCount++;

            //on reduit le scale avec du lerp
            transform.localScale = Vector3.Lerp(Vector3.one,Vector3.zero,resetCount/10f);

            if(resetCount > 10){
                Shutdown();
            }
        }
    }

    public void SetTargetPosition(Vector2Int pos){
        targetPosition = new Vector3(pos.x,pos.y,0);
    }

    //fonctions relative au bloc
    public virtual string GetName(){
        return "";
    }

    public GameObject GetCell(int x, int y, GameObject[,] plateau){
        if(x < 0 || x >= PlateauManager.instance.width || y < 0 || y >= PlateauManager.instance.width){
            return null;
        }
        return plateau[x,y];
    }

    public bool IsInBounds(int x, int y){
        return x >= 0 && x < PlateauManager.instance.width && y >= 0 && y < PlateauManager.instance.width;
    }

    public virtual DataSand[] GetNextStateData(int x, int y, GameObject[,] tableau){
        Visit();
        return null;
    }
    
}
