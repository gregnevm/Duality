using UnityEngine.Events;
using UnityEngine;

public static class CacheData 
{
    public static Vector3 MapCenter { get; private set; }
    public static Vector2Int TileSize { get; private set; }

    public static Tile PlayerCurrentTileLocation { get; private set; }
    
    
    static void SetTileSize((Vector3 mapCenter, Vector2Int tileSize) value)
    {
        MapCenter = value.mapCenter;
        TileSize = value.tileSize;
    }

    static void SetPlayerCurrentTileLocation(Tile tile)
    {

        PlayerCurrentTileLocation = tile;
    }


}
