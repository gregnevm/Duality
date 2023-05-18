using UnityEngine.UI;
using UnityEngine;

public class ItemImage : MonoBehaviour
{
    [SerializeField] StorageService parent;
    [SerializeField] Image cachedMyImage;
    Sprite standart;
    private void Awake()
    {
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        if ( parent.Item != null)
        {
            cachedMyImage.sprite = parent.Item.ItemSprite;
            Debug.Log("UPDATED SPRITE");
            return;
        }
        else
        {
            cachedMyImage.sprite = null;
            Debug.Log("Item is null");
        }
        

    }
}
