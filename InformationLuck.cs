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


            criticaldamageText.text = "치명타 적중률이 " + cripercent.ToString() + "% 증가하였습니다.";
            criticalText.text = "치명타 피해량이 " + cridamage.ToString("0.0") + "% 증가하였습니다.";
            getexpText.text = "경험치 획득률이 " + exp.ToString() + "% 증가하였습니다.";
            getequipText.text = "장비 획득률이 " + equip.ToString() + "% 증가하였습니다.";
        }
    }
}