using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the base class for all block interfaces in the game.
/// </summary>
public class BlocInterface : MonoBehaviour {

    // variables pour visité et mouvement
    protected bool visited = false;
    protected bool moving = false;

    //variables pour les sauvegardes
    bool toSave = false;
    public Vector2Int saveCoors;
    bool isDisapearing = false;
    int resetCount = 0;

    //variables pour les animations
    public Vector3 targetPosition;

    //variables pour la rotation
    public int indexRotation = 0;
    private int savedRotation = 0;
    public Sprite[] sprites;

    //fonctions de unity

    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    public void Start(){
        SetSprite();
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
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

    //fonctions pour eviter les repetitions

    /// <summary>
    /// Marks the block as visited.
    /// </summary>
    public virtual void Visit(){
        visited = true;
    }

    /// <summary>
    /// Resets the visited state of the block.
    /// </summary>
    public virtual void Reset(){
        visited = false;
    }

    /// <summary>
    /// Checks if the block has been visited.
    /// </summary>
    /// <returns>True if the block has been visited, false otherwise.</returns>
    public virtual bool IsVisited(){
        return visited;
    }

    //fonctions pour detecter les mouvements

    /// <summary>
    /// Sets the movement state of the block.
    /// </summary>
    /// <param name="state">The movement state to set.</param>
    public virtual void SetMoveState(bool state){
        moving = state;
    }

    /// <summary>
    /// Resets the movement state of the block.
    /// </summary>
    public virtual void ResetMoveState(){
        moving = false;
    }

    /// <summary>
    /// Checks if the block is currently moving.
    /// </summary>
    /// <returns>True if the block is moving, false otherwise.</returns>
    public virtual bool IsMoving(){
        return moving;
    }


    //fonctions pour les sauvegardes

    /// <summary>
    /// Saves the block's current position.
    /// </summary>
    /// <param name="coors">The coordinates to save.</param>
    public void Save(Vector2Int coors){
        toSave = true;
        saveCoors = coors;
        savedRotation = indexRotation;
    }

    /// <summary>
    /// Resets the save state of the block.
    /// </summary>
    public virtual void ResetSave(){
        toSave = false;
        isDisapearing = false;
        resetCount = 0;
        transform.localScale = Vector3.one;
        indexRotation = savedRotation;
        SetSprite();
        //on remet le bloc a sa place
        targetPosition = new Vector3(saveCoors.x,saveCoors.y,0);
    }

    /// <summary>
    /// Checks if the block needs to be saved.
    /// </summary>
    /// <returns>True if the block needs to be saved, false otherwise.</returns>
    public bool GetSave(){
        return toSave;
    }

    /// <summary>
    /// Makes the block disappear.
    /// </summary>
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

    

    /// <summary>
    /// Sets the target position for the block.
    /// </summary>
    /// <param name="pos">The target position.</param>
    public void SetTargetPosition(Vector2Int pos){
        targetPosition = new Vector3(pos.x,pos.y,0);
    }

    //fonctions relative au bloc

    /// <summary>
    /// Gets the name of the block.
    /// </summary>
    /// <returns>The name of the block.</returns>
    public virtual string GetName(){
        return "";
    }

    /// <summary>
    /// Gets the cell at the specified coordinates in the plateau.
    /// </summary>
    /// <param name="x">The x-coordinate of the cell.</param>
    /// <param name="y">The y-coordinate of the cell.</param>
    /// <param name="plateau">The plateau containing the cells.</param>
    /// <returns>The cell at the specified coordinates, or null if the coordinates are out of bounds.</returns>
    public GameObject GetCell(int x, int y, GameObject[,] plateau){
        if(x < 0 || x >= PlateauManager.instance.width || y < 0 || y >= PlateauManager.instance.width){
            return null;
        }
        return plateau[x,y];
    }

    /// <summary>
    /// Checks if the specified coordinates are within the bounds of the plateau.
    /// </summary>
    /// <param name="x">The x-coordinate to check.</param>
    /// <param name="y">The y-coordinate to check.</param>
    /// <returns>True if the coordinates are within the bounds of the plateau, false otherwise.</returns>
    public bool IsInBounds(int x, int y){
        return x >= 0 && x < PlateauManager.instance.width && y >= 0 && y < PlateauManager.instance.width;
    }

    /// <summary>
    /// Gets the next state data for the block at the specified coordinates in the tableau.
    /// </summary>
    /// <param name="x">The x-coordinate of the block.</param>
    /// <param name="y">The y-coordinate of the block.</param>
    /// <param name="tableau">The tableau containing the blocks.</param>
    /// <returns>An array of DataSand representing the next state data for the block.</returns>
    public virtual DataSand[] GetNextStateData(int x, int y, GameObject[,] tableau){
        Visit();
        return null;
    }
    
    //fonctions relatif a la rotation

    /// <summary>
    /// Rotates the block.
    /// </summary>
    public void Rotate(){
        indexRotation++;
        if(indexRotation >= sprites.Length){
            indexRotation = 0;
        }
        GetComponent<SpriteRenderer>().sprite = sprites[indexRotation];
    }

    private void SetSprite(){
        GetComponent<SpriteRenderer>().sprite = sprites[indexRotation];
    }
}
