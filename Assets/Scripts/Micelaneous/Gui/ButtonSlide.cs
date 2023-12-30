using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSlide : MonoBehaviour{
    public int index;
    public ButtonData[] buttonData;

    void Start(){
        SetActiveSprite(index);

        //on ajoute un listener pour chaque bouton
        for(int i = 0; i < buttonData.Length; i++){
            int index = i;
            buttonData[i].gameObject.GetComponent<Button>().onClick.AddListener(() => SetNewSprite(index));
        }
    }

    //fonctions pour modifier les sprites des boutons
    private void SetActiveSprite(int index){
        for(int i = 0; i < buttonData.Length; i++){
            if(i == index){
                buttonData[i].gameObject.GetComponent<Image>().sprite = buttonData[i].spritePressed;
            }else{
                buttonData[i].gameObject.GetComponent<Image>().sprite = buttonData[i].sprite;
            }
        }
    }

    //fonctions pour modifier les sprites des boutons
    public void SetNewSprite(int index){
        SetActiveSprite(index);
        this.index = index;
    }
}
