using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameController : MonoBehaviour
{
    [SerializeField] private bool _useMapGenerator;
    [SerializeField] private bool _saveMapToMapDataWhenGenerateNew;
    [SerializeField] MapGenerator _mapGenerator;
    [SerializeField] MapData _mapData;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LoadMap()
    {

        if (_useMapGenerator || _mapData == null)
        {
            _mapGenerator.GenerateAllMap();
            if (_saveMapToMapDataWhenGenerateNew)
            {
                _mapData = _mapGenerator.SaveMapDataToScriptableObject();
                SaveMap();
            }
        }
        else
        {
            foreach (var tile in _mapData.Tiles)
            {
                Instantiate(tile);
            }
        }
    }
    void SaveMap()
    {
        AssetDatabase.CreateAsset(_mapData, $"Assets/Resources/MapSaves/" + _mapData.GetType().Name.Replace(" ", "")+".asset");
        AssetDatabase.SaveAssets();
    }
}
