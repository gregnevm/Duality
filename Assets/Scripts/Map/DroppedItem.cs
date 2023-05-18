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
    public bool DropFromMining(Item item, Transform spawnLocation)
    {
        DroppedItem miniItem = Instantiate(item.MiniItem,spawnLocation);
        miniItem.transform.localPosition = new Vector3(Random.Range(-1f, 1f)*CacheData.TileSize.x, 0, Random.Range(-1f, 1f)*CacheData.TileSize.y);
        return miniItem ? true : false;
    }
}
