using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image itemIcon;
    public int itemCount;
    public Text CountText;

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImg;
        if (item.itemType != ItemType.Equipment)
            CountText.text = itemCount.ToString();
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        item = null;
        //text_Count.text = null;
        itemIcon.gameObject.SetActive(false);
    }

    public void SetSlotCount(int count)
    {
        itemCount += count;
    }
}
