using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/MapData")]
public class MapData : ScriptableObject
{
    [SerializeField] private int _seed;
    [SerializeField] private Biome[] _biomes;
    [SerializeField] private Color[] _biomeColors;
    [SerializeField] private List<Tile> _anchorTiles;
    [SerializeField] private Tile[,] _tiles;

    public int Seed { get => _seed; set => _seed = value; }
    public Tile[,] Tiles { get => _tiles; set => _tiles = value; }

    public (int seed, Biome[] biomes, Color[] colors, List<Tile> anchorTiles, Tile[,] tiles) GetData()
    {
        return (Seed, _biomes, _biomeColors, _anchorTiles, Tiles);
    }

    public void SaveData((int seed, Biome[] biomes, Color[] colors, List<Tile> anchorTiles, Tile[,] tiles) data)
    {
        Seed = data.seed;
        _biomes = data.biomes;
        _biomeColors = data.colors;
        _anchorTiles = data.anchorTiles;
        Tiles = data.tiles;
    }
}
