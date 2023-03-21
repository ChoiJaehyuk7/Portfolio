using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class InformationLuck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject PanelLuckInfo;
    public TextMeshProUGUI criticaldamageText, criticalText, getexpText, getequipText;
    public bool IsLuckInfo = false;

    Status status;
    InformationMP InfoMp;
    InformationDex InfoDex;
    InformationHP InfoHp;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        IsLuckInfo = !IsLuckInfo;
        PanelLuckInfo.SetActive(IsLuckInfo);

        if (IsLuckInfo)
        {
            InfoMp.PanelMpInfo.SetActive(false);
            InfoMp.IsMpInfo = false;

            InfoDex.PanelDexInfo.SetActive(false);
            InfoDex.IsDexInfo = false;

            InfoHp.PanelHpInfo.SetActive(false);
            InfoHp.IsHpInfo = false;
        } 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PanelLuckInfo.SetActive(false);
        IsLuckInfo = false;
    }

    // Start is called before the first frame update 
    void Start()
    {
        PanelLuckInfo.SetActive(IsLuckInfo);

        status = FindObjectOfType<Status>();
        InfoMp = FindObjectOfType<InformationMP>();
        InfoDex = FindObjectOfType<InformationDex>();
        InfoHp = FindObjectOfType<InformationHP>();
    }
    void Update()
    {
        if (IsLuckInfo)
        {
            float cripercent = status.Calcritical;
            float cridamage = status.Calcriticaldamage;
            float exp = status.Calgetexp;
            float equip = status.Calgetequip;


            criticaldamageText.text = "ġ��Ÿ ���߷��� " + cripercent.ToString() + "% �����Ͽ����ϴ�.";
            criticalText.text = "ġ��Ÿ ���ط��� " + cridamage.ToString("0.0") + "% �����Ͽ����ϴ�.";
            getexpText.text = "����ġ ȹ����� " + exp.ToString() + "% �����Ͽ����ϴ�.";
            getequipText.text = "��� ȹ����� " + equip.ToString() + "% �����Ͽ����ϴ�.";
        }
    }
}