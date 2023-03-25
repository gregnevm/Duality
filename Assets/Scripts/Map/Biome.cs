using UnityEngine;

[CreateAssetMenu(fileName = "Biome", menuName = "ScriptableObjects/Biomes", order = 1)]
public class Biome : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] Color _mainColor;
    public Color MainColor { get{ return _mainColor; } }

    public int BiomeType { get; internal set; }
}

