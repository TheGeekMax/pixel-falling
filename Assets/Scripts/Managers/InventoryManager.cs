using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour{
    private bool initialized = false;
    public static InventoryManager instance;

    public GameObject blocPrefab;
    public GameObject inventorySand;
    public GameObject inventoryMover;
    public GameObject inventoryColor;
    public GameObject inventorySplitter;

    public int[] inventoryCount;

    public GameObject[] inventory;

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
        inventory = new GameObject[BlocManager.instance.GetLength()];
        for(int i = 0; i < BlocManager.instance.GetLength(); i++){
            switch(BlocManager.instance.GetBlocData(i).type){
                case BlocType.Sand:
                    GameObject sand = Instantiate(blocPrefab,inventorySand.transform);
                    inventory[i] = sand;
                    sand.GetComponent<BlocButton>().SetSprite(BlocManager.instance.GetBlocData(i).prefab.GetComponent<SpriteRenderer>().sprite);
                    sand.GetComponent<BlocButton>().SetIndice(i);
                    break;
                case BlocType.Mover:
                    GameObject mover = Instantiate(blocPrefab,inventoryMover.transform);
                    inventory[i] = mover;
                    mover.GetComponent<BlocButton>().SetSprite(BlocManager.instance.GetBlocData(i).prefab.GetComponent<SpriteRenderer>().sprite);
                    mover.GetComponent<BlocButton>().SetIndice(i);
                    break;
                case BlocType.ColorChanger:
                    GameObject color = Instantiate(blocPrefab,inventoryColor.transform);
                    inventory[i] = color;
                    color.GetComponent<BlocButton>().SetSprite(BlocManager.instance.GetBlocData(i).prefab.GetComponent<SpriteRenderer>().sprite);
                    color.GetComponent<BlocButton>().SetIndice(i);
                    break;
                case BlocType.Spliter:
                    GameObject spliter = Instantiate(blocPrefab,inventorySplitter.transform);
                    inventory[i] = spliter;
                    spliter.GetComponent<BlocButton>().SetSprite(BlocManager.instance.GetBlocData(i).prefab.GetComponent<SpriteRenderer>().sprite);
                    spliter.GetComponent<BlocButton>().SetIndice(i);
                    break;
            }
        }

        inventoryCount = new int[BlocManager.instance.GetLength()];
        for(int i = 0; i < inventoryCount.Length; i++){
            if(SandBoxManager.instance.isSandbox){
                Set(i,SandBoxManager.instance.maxBlockCount);
            }else{
                Set(i,CodeManager.instance.GetInventoryCount(i));
            }
            inventory[i].GetComponent<BlocButton>().SetCount(inventoryCount[i]);
        }

        initialized = true;
    }


    public bool UseIfPossible(int indice){
        if(inventoryCount[indice] > 0){
            inventoryCount[indice] --;
            inventory[indice].GetComponent<BlocButton>().SetCount(inventoryCount[indice]);

            if(inventoryCount[indice] == 0){
                inventory[indice].SetActive(false);
            }
            return true;
        }
        return false;
    }

    public void Add(int indice){
        inventoryCount[indice] ++;
        inventory[indice].SetActive(true);
        inventory[indice].GetComponent<BlocButton>().SetCount(inventoryCount[indice]);
    }

    public void Set(int indice, int value){
        inventoryCount[indice] = value;
        inventory[indice].GetComponent<BlocButton>().SetCount(inventoryCount[indice]);
        if(value == 0){
            inventory[indice].SetActive(false);
        }else{
            inventory[indice].SetActive(true);
        }
    }
}
