using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory    : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject InvenPanel;
    public bool activeInven = false;

    public List<Item> items = new List<Item>();
    public delegate void ChangeItem();
    public ChangeItem C_Item;

    public Slot[] slots;
    public Image[] itemSlotimg;
    public Transform slotHolder;
    int GetCount;

    Inventory inven;
    delegate void OnSlot(int val);
    OnSlot onslot;
    int num;

    void Start()
    {
        inven = Inventory.instance;
        inven.C_Item += RedrawSlotUI;

        slots = slotHolder.GetComponentsInChildren<Slot>();
        InvenPanel.SetActive(activeInven);
    }

    // Update is called once per frame
    void Update()
    {
        Activation();
    }
     
    void Activation()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInven = !activeInven;
            InvenPanel.SetActive(activeInven);
            InvenPanel.transform.SetAsLastSibling();        
        }
    }
    void SlotInvoke()
    {
        if (C_Item != null)
            C_Item.Invoke();
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i].RemoveSlot();

        for(int i = 0; i<items.Count; i++)
        {
            slots[i].item = items[i];      
            slots[i].UpdateSlotUI();
        }
    }

    public void AcquireItem(Item item, int count = 1)//아이템 습득 -> 인벤토리 슬롯, 아이템 슬롯 저장
    {     
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null) //Null Reference 에러 초기화
            {
                if (slots[i].item.itemImg != null) // i 번째 슬롯에 아이템이 있다면
                {  
                    if (slots[i].item.itemName.Equals(item.itemName)) // i 번째 슬롯의 아이템 == 습득한 아이템
                    {                
                        if (item.itemType != ItemType.Equipment)
                        {
                            slots[i].itemCount += count; //해당 슬롯의 아이템 개수를 1만큼 증가시킨다.
                            SlotInvoke();
                            break;
                        }
                        else
                        {
                            items.Add(item);
                            slots[i + 1].itemCount = count;
                            if (item.itemID < 4)
                            {
                                itemSlotimg[num].GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                                num += 1;
                            }
                            else
                            {
                                itemSlotimg[num].GetComponent<RectTransform>().sizeDelta = new Vector2(90, 90);
                                num += 1;
                            }
                            SlotInvoke();
                            break;
                        }
                    }
                    
                    if(i == items.Count - 1) //습득한 아이템과 동일한 아이템이 인벤토리에 없을 경우
                    {
                        items.Add(item);
                        slots[i + 1].itemCount = count;;
                        if (item.itemID < 4)
                        {
                            itemSlotimg[i + 1].GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                            num += 1;
                        }
                        else
                        {
                            itemSlotimg[i + 1].GetComponent<RectTransform>().sizeDelta = new Vector2(90, 90);
                            num += 1;
                        }
                        SlotInvoke();
                        break;
                    }
                }
                else // 인벤토리가 비어있다면
                {
                    items.Add(item);
                    slots[i].itemCount = count;
                    num += 1;
                    if (item.itemID < 4)
                        itemSlotimg[i].GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                    else
                        itemSlotimg[i].GetComponent<RectTransform>().sizeDelta = new Vector2(90, 90);
                    SlotInvoke();
                    break;
                }
            }   
        }       
    }
}
