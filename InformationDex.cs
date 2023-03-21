using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class InformationDex : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject PanelDexInfo;
    public TextMeshProUGUI castingText, speedText, evasionText, coolText;
    public bool IsDexInfo = false;

    Status status;
    InformationMP InfoMp;
    InformationHP InfoHp;
    InformationLuck InfoLuck;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        IsDexInfo = !IsDexInfo;
        PanelDexInfo.SetActive(IsDexInfo);

        if (IsDexInfo)
        {
            InfoMp.PanelMpInfo.SetActive(false);
            InfoMp.IsMpInfo = false;

            InfoHp.PanelHpInfo.SetActive(false);
            InfoHp.IsHpInfo = false;

            InfoLuck.PanelLuckInfo.SetActive(false);
            InfoLuck.IsLuckInfo = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PanelDexInfo.SetActive(false);
        IsDexInfo = false;
    }

    // Start is called before the first frame update 
    void Start()
    {
        PanelDexInfo.SetActive(IsDexInfo);

        status = FindObjectOfType<Status>();
        InfoMp = FindObjectOfType<InformationMP>();
        InfoHp = FindObjectOfType<InformationHP>();
        InfoLuck = FindObjectOfType<InformationLuck>();
    }
    void Update()
    {
        if (IsDexInfo)
        {
            float casting = status.Calcasting;
            float speed = status.Calspeed;
            float evasion = status.Calevasion;
            float cooltime = status.Calcool;

            castingText.text = "��ų �����ӵ��� " + casting.ToString() + "% �����Ͽ����ϴ�.";
            speedText.text = "�̵� �ӵ��� " + speed.ToString() + "% �����Ͽ����ϴ�.";
            evasionText.text = "ȸ�Ƿ��� " + evasion.ToString() + "% �����Ͽ����ϴ�.";
            coolText.text = "��ų ���� ���ð��� " + cooltime.ToString() + "% �����Ͽ����ϴ�.";
        }
    }
}
