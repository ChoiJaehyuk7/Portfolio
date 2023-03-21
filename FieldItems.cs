using UnityEngine;
using TMPro;

public class FieldItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer img;
    public TextMeshProUGUI itemNameText;
    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImg = _item.itemImg;
        item.itemType = _item.itemType;
        item.color = _item.color;
        img.sprite = _item.itemImg;
        item.itemID = _item.itemID;
        itemNameText.text = item.itemName;
        itemNameText.color = item.color;
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
