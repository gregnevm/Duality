using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSerializableList : MonoBehaviour
{
   
    
}
[Serializable]
public struct CraftList
{
    public Item item;
    public float pieces;
    public float weight;
    public float size;

    public CraftList(Item item, float pieces, float weight, float size)
    {
        this.item = item;
        this.pieces = pieces;
        this.weight = weight;
        this.size = size;
    }
}
[Serializable]
public struct ItemDataList
{
    public Item item;
    public float pieces;
    public float weight;
    public float size;
    public ItemDataList(Item item, float pieces, float weight, float size)
    {
        this.item = item;
        this.pieces = pieces;
        this.weight = weight;
        this.size = size;
    }
}
