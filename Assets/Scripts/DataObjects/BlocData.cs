using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlocType{
    Sand,
    Mover,
    ColorChanger,
    Spliter
}

[System.Serializable]
public class BlocData{
    public string name;
    public string id;
    public BlocType type;
    public GameObject prefab;
}
