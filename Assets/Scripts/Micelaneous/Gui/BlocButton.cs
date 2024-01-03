using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlocButton : MonoBehaviour{
    int indice;
    public GameObject child;

    public void SetSprite(Sprite sprite){
        child.GetComponent<Image>().sprite = sprite;
    }

    public void SetIndice(int indice){
        this.indice = indice;
    }

    public void BlocClic(){
        ToolManager.instance.ChangeBloc(indice);
    }
}
