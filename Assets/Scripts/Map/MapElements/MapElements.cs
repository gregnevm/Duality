using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct BiomeChancesToSpawn
{
    public Biome biome;
    [Range(0, 1)] public float chance;
}
public class MapElements : MonoBehaviour
{    
    [SerializeField] List<BiomeChancesToSpawn> BiomeChancesToSpawns { get; set; }


    
}
