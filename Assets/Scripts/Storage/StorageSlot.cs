using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StorageSlot : MonoBehaviour, IStorageable, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private string _slotName;
    [SerializeField] private List<Item> _acceptedTypes = new List<Item>();
    [SerializeField] private Image _image;
    [SerializeField] private Transform _parentTransform;

    private Item _item;
    private float _currentWeight;
    private float _currentSizeM3;
    private float _currentPieces;

    public Item Item
    {
        get => _item;
        private set
        {
            _item = value;
            CurrentDataRefresh();
        }
    }

    private void Start()
    {
        CurrentDataRefresh();
    }

    public bool DropItem(Item item)
    {
        if (item == null || _item != null || item.MiniItem == null || !DroppedItem.DropFromStorage(this, item))
        {
            Debug.LogError("Drop Error");
            return false;
        }

        Item = item;
        return true;
    }

    public void OpenStorage()
    {
        // TODO: Implement storage opening logic
    }

    public bool SendMyItem(IStorageable storage)
    {
        if (storage.AcceptItem(Item))
        {
            Item = null;
            return true;
        }

        return false;
    }

    public bool AcceptItem(Item newItem)
    {
        if (_item == null && CheckItemTypeInAcceptedTypesList(newItem))
        {
            Item = newItem;
            return true;
        }

        if (newItem.GetType() == _item.GetType())
        {
            _item.AddItemValues(newItem);
            return true;
        }

        return false;
    }

    public void CurrentDataRefresh()
    {
        if (_item != null)
        {
            ItemDataList dataList = _item.GetItemDataList();
            _image.sprite = _item.ItemSprite;
            _currentSizeM3 = dataList.size;
            _currentWeight = dataList.weight;
            _currentPieces = dataList.pieces;
        }
        else
        {
            ClearSlot();
        }
    }

    public bool ReplaceItemsBetweenSlots(StorageSlot slot)
    {
        if (CheckSlotsItemsCanReplace(slot))
        {
            Item thisItem = Item;
            Item = slot.Item;
            slot.Item = thisItem;
            return true;
        }

        return false;
    }

    private bool CheckItemTypeInAcceptedTypesList(Item item)
    {
        if (_acceptedTypes.Count > 0 && _acceptedTypes.Contains(item))
        {
            return true;
        }

        return false;
    }

    private bool CheckSlotsItemsCanReplace(StorageSlot slot)
    {
        return CheckItemTypeInAcceptedTypesList(slot.Item) && slot.CheckItemTypeInAcceptedTypesList(Item);
    }

    private void ClearSlot()
    {
        _currentSizeM3 = 0;
        _currentWeight = 0;
        _currentPieces = 0;
    }

    public Transform GetTransform()
    {
        return _parentTransform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.transform.SetAsFirstSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _image.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.transform.position = transform.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        StorageSlot droppedSlot = eventData.pointerDrag.GetComponent<StorageSlot>();
        if (droppedSlot != null && droppedSlot != this)
        {
            if (ReplaceItemsBetweenSlots(droppedSlot))
            {
                eventData.pointerDrag.transform.position = transform.position;
            }
        }
    }
}