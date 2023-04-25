using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject _gridPrefab;

    Tile _centerOfGrid;
    Vector2Int _tileSize = new Vector2Int(10,10);
    Vector3 _centerOfMap;
    float _visionDistance = 100f;

    private void Awake()
    {
        EventBus.OnShowGridOnMap.AddListener(ShowGrid);
        EventBus.OnHideGridOnMap.AddListener(HideGrid);
        EventBus.OnNewMapCreated.AddListener(SetSizeOfTileAndMapcenter);
        EventBus.OnPlayerVisionDistanceChanged.AddListener(SetVisionDistance);
    }

    public void ShowGrid(Tile centralTileOfGrid)
    {
        EventBus.OnPlayerVisibleDistanceRequest.Invoke();

        DrawGridAreoundCentertile(centralTileOfGrid);
    }

    public void HideGrid()
    {

       
    }

    public void DestroyGrid()
    {
       
    }

    public void DrawGridAreoundCentertile(Tile centerTile)
    {
        Vector2Int tileSize = _tileSize;
        Vector3 centerPosition = centerTile.transform.position;
        float drawDistance = _visionDistance;
        GameObject gridPrefab = _gridPrefab;

        // Determine the number of grid tiles in each direction
        int numTilesX = Mathf.CeilToInt(drawDistance / tileSize.x) * 2 + 1;
        int numTilesZ = Mathf.CeilToInt(drawDistance / tileSize.y) * 2 + 1;

        // Instantiate the grid prefab and set its parent to the center tile's transform
        GameObject gridParent = new GameObject("Grid");
        gridParent.transform.SetParent(centerTile.transform);
        gridParent.transform.localPosition = Vector3.zero;

        for (int x = -numTilesX / 2; x <= numTilesX / 2; x++)
        {
            for (int z = -numTilesZ / 2; z <= numTilesZ / 2; z++)
            {
                // Calculate the position of the current grid tile
                Vector3 position = centerPosition + new Vector3(x * tileSize.x, 0f, z * tileSize.y);

                // Instantiate a copy of the grid prefab at the calculated position
                GameObject gridTile = Instantiate(gridPrefab, position, Quaternion.identity);

                // Set the parent of the grid tile to the grid parent object
                gridTile.transform.SetParent(gridParent.transform);
            }
        }
    }






    void SetSizeOfTileAndMapcenter( (Vector3 centerOfMap, Vector2Int size) value)
    {
        _tileSize = value.size;
        _centerOfMap = value.centerOfMap;        
    }
    void SetVisionDistance(float distance)
    {
        _visionDistance = distance;
    }
}