using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class MapElements : MonoBehaviour
{
    [SerializeField] Vector2Int _tileSize;
    [SerializeField] Tile _mainTile;
    [SerializeField] Tile _secondTileDirection;    
}
