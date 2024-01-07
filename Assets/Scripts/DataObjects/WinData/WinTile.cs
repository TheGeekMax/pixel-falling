using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class WinTile{
    public Tile tile;
    public Sprite sprite;

    [Header("WinBloc data")]
    public bool isColored = false;
    public Vector3 color;
}
