using UnityEngine;

public class DroppedItem : MonoBehaviour, IPickable
{
    Item Item;
    public void PickUp(IStorageable storage)
    {
        if (storage.AcceptItem(Item))
        {
            Destroy(this.gameObject);
        }
    }
   
    public static bool DropFromStorage(IStorageable DropFromStorage, Item droppedItem)
    {
        DroppedItem miniItem = Instantiate(droppedItem.MiniItem, DropFromStorage.GetTransform());
        miniItem.Item = droppedItem;
        if (miniItem != null &&  miniItem.Item != null)
        {
            return true;
        }
        return false;
    }
}
