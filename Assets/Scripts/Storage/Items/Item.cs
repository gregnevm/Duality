using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;



[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items/New Item")]
public class Item : ScriptableObject 
{
    [SerializeField] private Sprite _itemImage;
    [SerializeField] private float _pieces;
    [SerializeField] private float _weightPerPiece;
    [SerializeField] private float _sizePerPiece;
    [SerializeField] private DroppedItem _miniItemPrefab;
    [SerializeField] bool _isCraftable;
    [SerializeField] private List<CraftList> CraftList;

    

    private float _currentSizeM3;
    private float _currentWeight;
    private float _currentPieces;

    public Sprite ItemSprite { get => _itemImage;}
    public DroppedItem MiniItem { get => _miniItemPrefab; }
    public StorageSlot StorageSlot { get; private set; }
    

    public ItemDataList GetItemDataList()
    {
        ItemDataList itemDataList = new ItemDataList(this, this._currentPieces, this._currentWeight, this._currentSizeM3);
        return itemDataList;
    }
    public bool AddItemValues(Item item)
    {
        if (this.GetType() == item.GetType())
        {
            ItemDataList itemData = item.GetItemDataList();
            this._currentPieces += itemData.pieces;
            this._currentWeight += itemData.weight;
            this._currentSizeM3 += itemData.size;
            return true;
        }
        else
        {
            Debug.Log("Wrong Type of item");
            return false;
        }
        
    }
    





}

[CreateAssetMenu(fileName = "Equipment", menuName = "ScriptableObjects/Items/Equipment/New Equipment Item")]
public class Equipment : Item { }
[CreateAssetMenu(fileName = "RightHand", menuName = "ScriptableObjects/Items/Equipment/RightHand")]
public class RightHand : Equipment { }
[CreateAssetMenu(fileName = "LeftHand", menuName = "ScriptableObjects/Items/Equipment/LeftHand")]
public class LeftHand : Equipment { }
[CreateAssetMenu(fileName = "Head", menuName = "ScriptableObjects/Items/Equipment/Head")]
public class Head : Equipment { }
[CreateAssetMenu(fileName = "Chest", menuName = "ScriptableObjects/Items/Equipment/Chest")]
public class Chest : Equipment { }
[CreateAssetMenu(fileName = "Belt", menuName = "ScriptableObjects/Items/Equipment/Belt")]
public class Belt : Equipment { }
[CreateAssetMenu(fileName = "Legs", menuName = "ScriptableObjects/Items/Equipment/Legs")]
public class Legs : Equipment { }
[CreateAssetMenu(fileName = "Boots", menuName = "ScriptableObjects/Items/Equipment/Boots")]
public class Boots : Equipment { }
[CreateAssetMenu(fileName = "BackPack", menuName = "ScriptableObjects/Items/Equipment/BackPack")]
public class BackPack : Equipment { }
public class Weapon : Item { }
public class Food : Item { }
[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/Items/Resources/")]
public class Resource : Item { }
public class Component : Item { }
// /////////////////////////////////
public class Blueprint : Item { }
public class BuldingBlueprint : Blueprint { }
public class EquipmentBlueprint : Blueprint { }

