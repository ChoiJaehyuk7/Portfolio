using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class InformationMP : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject PanelMpInfo;
    public TextMeshProUGUI DamageText, MaxMpText, penetText, exploText, gaugeText, recoveryMpText;
    public bool IsMpInfo = false;

    InformationDex InfoDex;
    InformationHP InfoHp;
    InformationLuck InfoLuck;
    Status status;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        IsMpInfo = !IsMpInfo;
        PanelMpInfo.SetActive(IsMpInfo);

        if (IsMpInfo)
        {
            InfoDex.PanelDexInfo.SetActive(false);
            InfoDex.IsDexInfo = false;

            InfoHp.PanelHpInfo.SetActive(false);
            InfoHp.IsHpInfo = false;

            InfoLuck.PanelLuckInfo.SetActive(false);
            InfoLuck.IsLuckInfo = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PanelMpInfo.SetActive(false);
        IsMpInfo = false;
    }

    // Start is called before the first frame update 
    void Start()
    {
        PanelMpInfo.SetActive(IsMpInfo);

        status = FindObjectOfType<Status>();
        InfoDex = FindObjectOfType<InformationDex>();
        InfoHp =  FindObjectOfType<InformationHP>();
        InfoLuck = FindObjectOfType<InformationLuck>();
    }
    void Update()
    {
        if (IsMpInfo)
        {
            float damageAmount = status.Caldamage2;
            float MaxMP = status.CalmaxMp;
            float penet = status.Calpenet;
            float explo = status.Calexplo;
            float gauge = status.Calgauge;
            float recoveryMp = status.CalrecoveryMP;
            DamageText.text = "��ų ���ط��� " + damageAmount.ToString() + " �����Ͽ����ϴ�.";
            MaxMpText.text = "�ִ� ������ " + MaxMP.ToString() + " �����Ͽ����ϴ�.";
            penetText.text = "���� ������� " + penet.ToString() + " �����Ͽ����ϴ�.";
            exploText.text = "��ų ������ " + explo.ToString() + " �����Ͽ����ϴ�.";
            gaugeText.text = "���� �������� " + gauge.ToString() + " �����Ͽ����ϴ�.";
            recoveryMpText.text = "���� ȸ������ " + recoveryMp.ToString() + " �����Ͽ����ϴ�.";
        }
    }
}