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

            castingText.text = "스킬 시전속도가 " + casting.ToString() + "% 증가하였습니다.";
            speedText.text = "이동 속도가 " + speed.ToString() + "% 증가하였습니다.";
            evasionText.text = "회피력이 " + evasion.ToString() + "% 증가하였습니다.";
            coolText.text = "스킬 재사용 대기시간이 " + cooltime.ToString() + "% 감소하였습니다.";
        }
    }
}
