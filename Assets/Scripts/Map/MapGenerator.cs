using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    
    [SerializeField] int seed = 0;
    [SerializeField] int minRangeBetweenBiomesAncors, maxRangeBetweenBiomesAnchors;
    [SerializeField, Range(0.1f, 1)] float _sizeBiomesCoefficient;
    [SerializeField, Range(0.1f, 1)] float _smoothInfluenceCoefficient;
    [SerializeField] int _mapHeight, _mapWeight;
    [SerializeField] float _standartVisionRange;
    [SerializeField] Biome[] biomes;
    [SerializeField] Tile _tilePrefab;
    [SerializeField, Range(0,1)] float maxWeightOfMainBiome;


    Color[] _biomeColors;
    private List<Tile> _anchorTiles;
    private Tile[,] _tiles;

    public Color[] BiomeColors { get => _biomeColors; private set => _biomeColors = value; }

    private void Awake()
    {
        _anchorTiles = new();
        _tiles = new Tile[_mapWeight, _mapHeight];
        BiomeColors = new Color[biomes.Length];

        for (int i = 0; i < BiomeColors.Length; i++)
        {
            BiomeColors[i] = biomes[i].MainColor;
        }
    }
    void Start()
    {
        Random.InitState(seed);        
    }

    void GenerateMap()
    {
        GenerateAllMap();
    }

    public void GenerateAllMap()
    {
        GenerateAnchorTilesOfBiomes();
        // Знаходимо всі тайли, що ще не були згенеровані
        List<Tile> tilesToGenerate = new();
        for (int x = 0; x < _mapWeight; x++)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                if (_tiles[x, y] == null)
                {
                    tilesToGenerate.Add(new Tile(x, y));
                }
            }
        }
        
        // Генеруємо кожен з них
        while (tilesToGenerate.Count > 0)
        {
            Tile tile = tilesToGenerate[0];

            // Розраховуємо ваги на основі ваг сусідніх тайлів
            float[] weights = CalculateWeightsFromAnchorTilesWeights(tile);

            // Генеруємо новий тайл
            Tile newTile = SpawnNewTile(BiomeColors, weights, new Vector3(tile.X*10,0,tile.Y*10) );
            
            _tiles[tile.X, tile.Y] = newTile;
            if ((tile.X /2) > _standartVisionRange || (tile.Y/2) > _standartVisionRange)  
            {
                _tiles[tile.X, tile.Y].transform.gameObject.SetActive(false);
            }
            // Видаляємо тайл зі списку тайлів, які ще потрібно згенерувати
            tilesToGenerate.RemoveAt(0);
            
        }
    }
    private void GenerateAnchorTilesOfBiomes()
    {
        float sizeBiomesCoefficient = _sizeBiomesCoefficient;
        int weight = _mapWeight;
        int height = _mapHeight;
        // Generate first anchor Tile
        
        Tile newAnchorTile = SpawnNewAnchorTile(Vector3.zero);
        _tiles[0, 0] = newAnchorTile;
        newAnchorTile.Y = 0;
        newAnchorTile.X = 0;
        _anchorTiles.Add(newAnchorTile);

        // Generate additional anchor tiles
        int interval = (int)(weight * sizeBiomesCoefficient);
        for (int i = 0; i < weight; i += interval + (int)CalculateRandomDistance())
        {
            for (int j = 0; j < height; j +=  interval + (int)CalculateRandomDistance())
            {
                if (_tiles[i, j] == null) // Only place anchor tile if one does not already exist at this position
                {
                    Vector3 position = new Vector3(i*10, 0, j*10);                  
                   
                    Tile newTile = SpawnNewAnchorTile(position);
                    newTile.X = i;
                    newTile.Y = j;
                    _tiles[newTile.X, newTile.Y] = newTile;
                    _anchorTiles.Add(newTile);
                    Debug.Log($"Anchor Tile created at X: {newTile.X} Y: {newTile.Y}");
                    
                }
            }
        }
    }

    private float[] CalculateWeightsFromAnchorTilesWeights(Tile tile)
    {
        var weights = new float[biomes.Length];
       
        var sumOfDistances = 0f;

        // calculate distance to each anchor tile
        var anchorDistances = new List<float>();
        foreach (var anchorTile in _anchorTiles)
        {
            var distance = Vector2.Distance(new Vector2(tile.X, tile.Y), new Vector2(anchorTile.X, anchorTile.Y));
            anchorDistances.Add(distance);
            sumOfDistances += distance;
        }
        // calculate influence of anchor tile distances on weights        
        for (var i = 0; i < anchorDistances.Count; i++)
        {
            var distance = anchorDistances[i];
            var influence = (sumOfDistances - distance) / (distance/_smoothInfluenceCoefficient);
            

            for (var j = 0; j < biomes.Length; j++)
            {
                weights[j] += _anchorTiles[i].BiomeWeights[j] * influence ;
            }
        }

        

        return WeightsNormalize( weights);
    }



    private List<Tile> GetNeighborTiles(int x, int y)
    {
        List<Tile> neighborTiles = new List<Tile>();
        int[,] directions = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 }, { -1, -1 }, { 1, 1 }, { 1, -1 }, { -1, 1 } };
        int weight = _tiles.GetLength(0);
        int height = _tiles.GetLength(1);

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int neighborX = x + directions[i, 0];
            int neighborY = y + directions[i, 1];

            if (neighborX >= 0 && neighborX < weight && neighborY >= 0 && neighborY < height && _tiles[neighborX, neighborY] != null && _tiles[neighborX, neighborY].BiomeWeights.Sum() > 0)
            {
                neighborTiles.Add(_tiles[neighborX, neighborY]);
            }
        }

        return neighborTiles;
    }
    float[] SetRandomWeightToBiomes()
    {
        int randomDominateBiome = Random.Range(0, biomes.Length);
        float[] weights = new float[biomes.Length];

        // Генеруємо випадкові ваги для кожного біома
        
        float dominateWeight = Random.Range(1f, biomes.Length);
        for (int i = 0; i < weights.Length; i++)
        {
            if (i == randomDominateBiome)
            {
                weights[i] = dominateWeight;
            }
            else
            {
                weights[i] = Random.Range(0f, 1f);
            }
            
        }

       
       return WeightsNormalize(weights);
    }

    private float[] WeightsNormalize(float[] weights)
    {
        float[] result = new float[weights.Length];
        float sum = 0;
        foreach (var weight  in weights)
        {
            sum += weight;
        }
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = weights[i] / sum;
        }
        return result;
    }

    Tile SpawnNewTile(Color[] colors, float[] weights, Vector3 position)
    {
        Tile tile = Instantiate(_tilePrefab, position, Quaternion.identity);        
        tile.BlendNewColorAndSetWeights(colors, weights);
        return tile;
    }
    private float CalculateRandomDistance()
    {
        
        float distance = Random.Range(minRangeBetweenBiomesAncors, maxRangeBetweenBiomesAnchors)* _sizeBiomesCoefficient;
        return distance; 
    }
    Tile SpawnNewAnchorTile(Vector3 position)
    {
        Color[] colors = _biomeColors;
        float[] weights = new float[biomes.Length];
        int indexOfAnchorBiome = Random.Range(0, biomes.Length);
        weights[indexOfAnchorBiome] = maxWeightOfMainBiome;
        float summOfAnotherWeights = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            if (i!= indexOfAnchorBiome && i != weights.Length)
            {
                weights[i] = Random.RandomRange(0, 1 - maxWeightOfMainBiome - summOfAnotherWeights);
                summOfAnotherWeights += weights[i];
            }
            else if (i == weights.Length)
            {
                weights[i] = 1 - summOfAnotherWeights;
            }
        }

        

        Tile tile = Instantiate(_tilePrefab, position, Quaternion.identity);
        tile.BlendNewColorAndSetWeights(colors, weights);
        return tile;
    }
    public MapData SaveMapDataToScriptableObject()
    {
        MapData mapData = ScriptableObject.CreateInstance<MapData>();
        mapData.SaveData((seed, biomes, _biomeColors, _anchorTiles, _tiles));
        return mapData;

    }
    
}

