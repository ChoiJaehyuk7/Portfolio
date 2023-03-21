using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SkillButton : MonoBehaviour
{
    public TextMeshProUGUI NameText, CurrentLvText, TypeText, AcquireLvText, MaxLvText, LeadingNameText, LeadingLvText,
        SpendSpText, DistanceText, SustainText, CoolTimeText, ExploText, SpendMpText, InfoText;
    public Image img;
    public int ID;
    SkillManager sm;
  
    public void PressSkillButton()
    {
        GameObject.Find("SkillManager").GetComponent<SkillStatus>().ssID = ID;
        TextUI(ID);
    }

    private void Start()
    {
        GameObject.Find("SkillManager").GetComponent<SkillStatus>().ssID = 0;
        TextUI(0);
    }

    void TextUI(int buttonID)
    {
        sm = FindObjectOfType<SkillManager>();

        img.sprite = sm.skills[buttonID].skillsprite.sprite;
        NameText.text = sm.skills[buttonID].Name;
        CurrentLvText.text = sm.skills[buttonID].CurrLv.ToString();
        if (sm.skills[buttonID].type == SkillType.Deal)
            TypeText.text = "공격 스킬";
        else if (sm.skills[buttonID].type == SkillType.Buff)
            TypeText.text = "버프 스킬";
        else
            TypeText.text = "패시브 스킬";

        AcquireLvText.text = sm.skills[buttonID].Acqulv.ToString();
        MaxLvText.text = sm.skills[buttonID].maxlv.ToString();
        if (sm.skills[buttonID].leadingskill == "")
        {
            LeadingNameText.text = "없음";
            LeadingLvText.text = " ";
        }
        else
        {
            LeadingNameText.text = sm.skills[buttonID].leadingskill;
            LeadingLvText.text = sm.skills[buttonID].leadingskillLv.ToString() + " 레벨";
        }
        SpendSpText.text = sm.skills[buttonID].sp.ToString();
        DistanceText.text = sm.skills[buttonID].distance.ToString();
        SustainText.text = sm.skills[buttonID].sustaintime.ToString("F1");
        CoolTimeText.text = sm.skills[buttonID].cooltime.ToString("F1");
        ExploText.text = sm.skills[buttonID].explo.ToString("F1");
        SpendMpText.text = sm.skills[buttonID].consumeMp.ToString("F1");
        InfoText.text = sm.skills[buttonID].infotext;
    }
}
