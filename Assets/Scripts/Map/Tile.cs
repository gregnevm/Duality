using System;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Tile : MonoBehaviour , ISelectable
{
    [SerializeField] Tile _prefab;
    [SerializeField] Color _color;    
    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] float[] _biomeWeights;
    [SerializeField] int x, y;
    [SerializeField] Vector2Int _tileSize;
    
    public int X { get; set; }
    public int Y { get; set; }
    public Vector2Int TileSize { get => _tileSize;  }
    

    public Biome AnchorBiome { get; private set; }
        
    public float[] BiomeWeights { get => _biomeWeights; private set => _biomeWeights = value; }

    public bool IsSelected   { get; private set ; }
    public Material TileMaterial { get => _tileMaterial ; private set => _tileMaterial = value; }
    public MeshRenderer MeshRenderer { get => _meshRenderer; private set => _meshRenderer = value; }

    private Material _tileMaterial;
   

    Color _biomeBlendColor;

    public Tile(int x, int y)
    {
        X = x;
        Y = y;
    }

    private void Awake()
    {     
        _tileMaterial = _meshRenderer.material;
        IsSelected = false;
        AutoSetTileSize();
    }
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
        _biomeBlendColor = new Color(r, g, b, a);
        SetNewColor( new Color(r, g, b, a));
        

    } 

    void SetNewColor(Color newColor)
    {
        _color = newColor;
        _tileMaterial.color = newColor;
    }

    void AutoSetTileSize()
    {        
        Vector3 meshSize = MeshRenderer.bounds.size;
        _tileSize = new Vector2Int(Mathf.RoundToInt(meshSize.x), Mathf.RoundToInt(meshSize.z));
    }
     public void Select()
    {
        IsSelected = true;        
        EventBus.OnTileSelected.Invoke(this);  
        
    }

     public void Deselect()
    {
        IsSelected = false;
        EventBus.OnTileDeselected.Invoke(this);
        
    }

    private void OnMouseDown()
    {
        //_=   GetIsSelected() ? Deselect() : Select(); // not working. Lol
        if (!IsSelected) Select();
        else if (IsSelected) Deselect();
        else Debug.Log($"Tile X: {x} Y: {y} have problems with ISelectable");
    }

    public bool GetIsSelected()
    {
        return IsSelected;
    }    
    public void SetBiomeColor()
    {
        SetNewColor(_biomeBlendColor);
    }

    
}
