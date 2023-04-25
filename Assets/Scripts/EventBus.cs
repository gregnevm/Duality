using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventBus
{
    public static UnityEvent<Tile> OnTileSelected = new();
    public static UnityEvent<Tile> OnTileDeselected = new();

    public static UnityEvent PlayerTileLocationRequest = new();
    public static UnityEvent<Tile> OnTilePlayerLocationSend = new();



    public static UnityEvent<Tile> OnShowGridOnMap = new();
    public static UnityEvent OnHideGridOnMap = new();

    public static UnityEvent<(Vector3, Vector2Int)> OnNewMapCreated = new();

    public static UnityEvent OnPlayerVisibleDistanceRequest = new();
    public static UnityEvent<float> OnPlayerVisionDistanceChanged = new();
}
