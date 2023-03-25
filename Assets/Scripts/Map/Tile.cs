using System;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Tile : MonoBehaviour
{
    [SerializeField] Tile _prefab;
    [SerializeField] Color _tileColor;
    [SerializeField] Material _tileMaterialPrefab;
    [SerializeField] float[] _biomeWeights;
    [SerializeField] int x, y;
    public Biome AnchorBiome { get; private set; }
    
    public int X { get; set; }
    public int Y { get; set; }

    private Material _tileMaterial;

    public Tile(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    private void Awake()
    {
        // створюємо копію матеріалу для кожного тайлу
        _tileMaterial = Instantiate(_tileMaterialPrefab);
        // присвоюємо матеріал кожному тайлу
        GetComponent<Renderer>().material = _tileMaterial;
    }

    public float[] BiomeWeights { get => _biomeWeights; private set => _biomeWeights = value; }


    public void BlendNewColorAndSetWeights(Color[] colors, float[] weights)
    {
        x = X;
        y = Y;
        if (colors == null || colors.Length == 0 || weights == null || weights.Length == 0 || colors.Length != weights.Length)
        {
            throw new ArgumentException("Invalid input parameters.");    
        }
        
        _biomeWeights = weights;        
        float weightSum = weights.Sum();
        
        if (weightSum <= 0)        
        {        
            throw new ArgumentException("Weights must be positive.");           
        }

            float r = 0f;
            float g = 0f;
            float b = 0f;
            float a = 0f;

            for (int i = 0; i < colors.Length; i++)
            {
                float weight = weights[i] / weightSum;
                r += colors[i].r * weight;
                g += colors[i].g * weight;
                b += colors[i].b * weight;
                a += colors[i].a * weight;
            }

            SetNewColor( new Color(r, g, b, a));
        

    }

    void SetNewColor(Color newColor)
    {
        _tileColor = newColor;
        _tileMaterial.color = newColor;
    }

   
}
