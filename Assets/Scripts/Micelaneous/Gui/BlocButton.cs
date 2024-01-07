using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlocButton : MonoBehaviour{
    int indice;
    public GameObject child;

    public GameObject textCount;

    public void SetSprite(Sprite sprite){
        child.GetComponent<Image>().sprite = sprite;
    }

    public void SetCount(int count){
        textCount.GetComponent<TextMeshProUGUI>().text = count.ToString();
    }

    public void SetIndice(int indice){
        this.indice = indice;
    }

    public void BlocClic(){
        ToolManager.instance.ChangeBloc(indice);
    }
}
