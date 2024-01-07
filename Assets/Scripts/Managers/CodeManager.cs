using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeManager : MonoBehaviour{
    private bool initialized = false;
    public static CodeManager instance;


    public string code = "";

    private Alphabet data64;

    int width, height;
    bool[,] lockedMap;
    int[,] winMap;
    int[,] plateauMap;

    int[] inventoryCount;

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
    }

    void Update(){
        if(!initialized) return;
    }

    public void Initialize(){
        data64 = new Alphabet(Alphabet.default64,6);

        //get width and height

        width = data64.Get(code[0])+1;
        height = data64.Get(code[1])+1;

        //get locked map

        lockedMap = new bool[width,height];

        int charIndex = 2;
        int index = 0;
        while(index < width*height){
            int x = index % width;
            int y = index / width;

            int[] values = data64.tenToBinary(data64.Get(code[charIndex]));
            charIndex ++;
            for(int i = 0; i < 6 && index < width*height; i ++){
                lockedMap[x,y] = values[i] == 1;
                index ++;
                x = index % width;
                y = index / width;
            }
        }

        //get win map

        winMap = new int[width,height];
        for(int i = 0; i < width; i ++){
            for(int j = 0; j < height; j ++){
                winMap[i,j] = -1;
            }
        }

        while(code[charIndex] != '-'){
            int x = data64.Get(code[charIndex]);
            charIndex ++;
            int y = data64.Get(code[charIndex]);
            charIndex ++;
            int id = data64.Get(code[charIndex]);
            charIndex ++;

            winMap[x,y] = id;
        }
        charIndex ++;

        //get plateau map
        plateauMap = new int[width,height];

        for(int i = 0; i < width; i ++){
            for(int j = 0; j < height; j ++){
                plateauMap[i,j] = -1;
            }
        }

        while(code[charIndex] != '-'){
            int x = data64.Get(code[charIndex]);
            charIndex ++;
            int y = data64.Get(code[charIndex]);
            charIndex ++;
            int id = data64.Get(code[charIndex]);
            charIndex ++;

            plateauMap[x,y] = id;
        }
        charIndex ++;

        //get inventory
        inventoryCount = new int[BlocManager.instance.GetLength()];
        for(int i = 0; i < inventoryCount.Length; i ++){
            inventoryCount[i] = 0;
        }

        while(charIndex < code.Length){
            int id = data64.Get(code[charIndex]);
            charIndex ++;
            int count = data64.Get(code[charIndex]);
            charIndex ++;

            inventoryCount[id] = count;
        }

        initialized = true;
    }

    //fonction pour encoder le niveau
    public void Encode(){
        //on transforme le niveaux en les valeurs des tableau
        width = PlateauManager.instance.width;
        height = PlateauManager.instance.height;

        lockedMap = new bool[width,height];
        winMap = new int[width,height];
        plateauMap = new int[width,height];
        inventoryCount = new int[BlocManager.instance.GetLength()];

        for(int i = 0; i < inventoryCount.Length; i ++){
            inventoryCount[i] = 0;
        }

        for(int i = 0; i < width; i ++){
            for(int j = 0; j < height; j ++){
                lockedMap[i,j] = PlateauManager.instance.IsPlaceable(new Vector2Int(i,j));
                winMap[i,j] = PlateauManager.instance.GetWinBloc(new Vector2Int(i,j)) == null ? -1 : 1;

                if(PlateauManager.instance.IsPlaceable(new Vector2Int(i,j))){
                    plateauMap[i,j] = -1;
                    if(PlateauManager.instance.GetBloc(i,j) != null){
                        inventoryCount[PlateauManager.instance.GetBlocId(i,j)] ++;
                    }
                }else{
                    plateauMap[i,j] = PlateauManager.instance.GetBlocId(i,j);
                }
            }
        }


        
        code = "";

        //encode width and height
        code += data64.Get(width-1);
        code += data64.Get(height-1);

        //encode locked map
        int index = 0;
        int charIndex = 2;
        while(index < width*height){
            int x = index % width;
            int y = index / width;

            int[] values = new int[6];
            for(int i = 0; i < 6 && index < width*height; i ++){
                values[i] = lockedMap[x,y] ? 1 : 0;
                index ++;
                x = index % width;
                y = index / width;
            }

            code += data64.Get(data64.binaryto10(values));
            charIndex ++;
        }

        code += "-";

        //encode win map
        for(int i = 0; i < width; i ++){
            for(int j = 0; j < height; j ++){
                if(winMap[i,j] != -1){
                    code += data64.Get(i);
                    code += data64.Get(j);
                    code += data64.Get(winMap[i,j]);
                }
            }
        }

        code += "-";

        //encode plateau map
        for(int i = 0; i < width; i ++){
            for(int j = 0; j < height; j ++){
                if(plateauMap[i,j] != -1){
                    code += data64.Get(i);
                    code += data64.Get(j);
                    code += data64.Get(plateauMap[i,j]);
                }
            }
        }

        code += "-";

        //encode inventory
        for(int i = 0; i < inventoryCount.Length; i ++){
            if(inventoryCount[i] != 0){
                code += data64.Get(i);
                code += data64.Get(inventoryCount[i]);
            }
        }
    }

    //fonctions pour recup les infos

    public int GetWidth(){
        return width;
    }

    public int GetHeight(){
        return height;
    }

    public bool IsLocked(Vector2Int coors){
        return lockedMap[coors.x,coors.y];
    }

    public int GetBlocId(Vector2Int coors){
        return plateauMap[coors.x,coors.y];
    }

    public int GetWinId(Vector2Int coors){
        return winMap[coors.x,coors.y];
    }

    public int GetInventoryCount(int id){
        return inventoryCount[id];
    }
}
