using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageService : MonoBehaviour, IDropHandler
{
    [SerializeField] List<Item> whiteTypeList = new List<Item>(); // List to accept items
    [SerializeField] List<Item> blackTypeList = new List<Item>(); // List of types unaccepted in storage
    [SerializeField] List<StorageService> childStorageServices = new List<StorageService>();
    

    [SerializeField] private Item _item;
    [SerializeField] ItemImage itemImage;

    public Item Item { get => _item; }   

    public bool GetNewItem(Item item)
    {
        if (item == null)
        {
            Debug.Log("Getted item is null. Return true");
            UpdateData();
            return true;
        }

        if (CheckToGetItemIsPossible(item))
        {
            

            if (IsItemHaveSameType(item))
            {
                AddItemsValues(item);
                UpdateData();
                return true;
            }
            else if (_item == null)
            {
                _item = item;
                UpdateData();
                return true;
            }
        }

        (bool success, StorageService storage) = CheckToGetItemIsPossibleInChilds(item);
        if (success)
        {
            return storage.GetNewItem(item);
        }
        UpdateData();
        return false;
    }

    private void AddItemsValues(Item item)
    {
        Debug.Log("Add items values logic");

    }

    private void ReplaceItemsRequest(StorageService storage)
    {
        if (storage != null)
        {
            Item myItem = _item;
            Item newItem = storage._item;
            _item = null;
            storage._item = null;
            if (storage.GetNewItem(myItem) && GetNewItem(newItem))
            {
                Debug.Log("Replace completed");

                return;
            }
            _item = myItem;
            storage._item = newItem;
            UpdateData();
            storage.UpdateData();
            Debug.Log("Replace not completed");
        }
    }

    bool CheckTypesInWhiteAndBlackList(Item item)
    {
        foreach (Item blackType in blackTypeList)
        {
            if (blackType.GetType() == item.GetType())
            {
                Debug.Log("Item in black list");

                return false;
            }
        }

        foreach (Item whiteType in whiteTypeList)
        {
            if (whiteType.GetType() == item.GetType())
            {
                Debug.Log("item in white list");

                return true;
            }
        }
        Debug.Log("Item not in white list");

        return false;
    }

    public bool CheckToGetItemIsPossible(Item item)
    {
        if ((_item == null && CheckTypesInWhiteAndBlackList(item)) || IsItemHaveSameType(item))
        {
            return true;
        }

        return false;
    }

    public (bool, StorageService) CheckToGetItemIsPossibleInChilds(Item item)
    {
        foreach (var storage in childStorageServices)
        {
            if (storage.CheckToGetItemIsPossible(item))
            {
                return (true, storage);
            }

            (bool success, StorageService childStorage) = storage.CheckToGetItemIsPossibleInChilds(item);
            if (success)
            {
                return (true, childStorage);
            }
        }

        return (false, null);
    }

    private bool IsItemHaveSameType(Item item)
    {
        if (_item != null && _item.GetType() == item.GetType())
        {
            Debug.Log("is same type item");

            return true;
        }
        return false;
    }

    private void UpdateData()
    {
        itemImage.UpdateSprite();
    }

    public void AddWhiteType(Item whiteType)
    {
        whiteTypeList.Add(whiteType);
    }

    public void RemoveWhiteType(Item whiteType)
    {
        whiteTypeList.Remove(whiteType);
    }

    public void AddBlackType(Item blackType)
    {
        blackTypeList.Add(blackType);
    }

    public void RemoveBlackType(Item blackType)
    {
        blackTypeList.Remove(blackType);
    }  

    public void OnDrop(PointerEventData eventData)
    {
        StorageService otherStorageService = eventData.pointerDrag.GetComponent<StorageService>();

        if (otherStorageService != null && otherStorageService != this)
        {
            Debug.Log("Start replacing items");

            ReplaceItemsRequest(otherStorageService);
            UpdateData();
        }
    }
}
