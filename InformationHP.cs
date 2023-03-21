using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class InformationHP : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject PanelHpInfo;
    public TextMeshProUGUI MaxHpText, armorText, imunityText, recoveryText;
    public bool IsHpInfo = false;

    Status status;
    InformationMP InfoMp;
    InformationDex InfoDex;
    InformationLuck InfoLuck;
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHpInfo = !IsHpInfo;
        PanelHpInfo.SetActive(IsHpInfo);

        if (IsHpInfo)
        {
            InfoMp.PanelMpInfo.SetActive(false);
            InfoMp.IsMpInfo = false;

            InfoDex.PanelDexInfo.SetActive(false);
            InfoDex.IsDexInfo = false;

            InfoLuck.PanelLuckInfo.SetActive(false);
            InfoLuck.IsLuckInfo = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PanelHpInfo.SetActive(false);
        IsHpInfo = false;
    }

    // Start is called before the first frame update 
    void Start()
    {
        PanelHpInfo.SetActive(IsHpInfo);

        status = FindObjectOfType<Status>();
        InfoMp = FindObjectOfType<InformationMP>();
        InfoDex = FindObjectOfType<InformationDex>();
        InfoLuck = FindObjectOfType<InformationLuck>();
    }
    void Update()
    {
        if (IsHpInfo)
        {
            float maxhp = status.CalmaxHp;
            float armor = status.Calarmor;
            float imunity = status.Calimunity;
            float recovery = status.Calrecovery;


            MaxHpText.text = "�ִ� ������� " + maxhp.ToString() + " �����Ͽ����ϴ�.";
            armorText.text = "������ " + armor.ToString() + "% �����Ͽ����ϴ�.";
            imunityText.text = "�鿪���� " + imunity.ToString() + " �����Ͽ����ϴ�.";
            recoveryText.text = "ü�� ȸ���ӵ��� " + recovery.ToString() + "% �����Ͽ����ϴ�.";
        }
    }
}