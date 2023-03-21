using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill14 : MonoBehaviour
{
    public KeyCode SkillCode = KeyCode.None;
    bool IsSkill;
    public Image coolimg;
    public TextMeshProUGUI cooltxt;
    float cooltimer;
    [HideInInspector] public SkillManager sm;
    [HideInInspector] public Status status;
    [HideInInspector] public PlayerHP ph;
    [HideInInspector] public PlayerMove pm;
    [HideInInspector] public PlayerTrigger pt;

    public int PlusCritical;
    void Update()
    {
        Use();
    }

    void Use()
    {
        if (Input.GetKeyDown(SkillCode) && !IsSkill && ph.MP >= sm.skills[14].consumeMp)//스킬코드를 누르고 스킬 사용중이 아니며 스킬 사용가능한 마나가 있을 때
        {
            ph.MP -= sm.skills[14].consumeMp;
            float incScale = status.Calexplo2 + sm.skills[12].explo * 0.01f;

            IsSkill = true;
        }

        if (IsSkill)
        {
            PlusCritical = 10;
            float cooltime = sm.skills[14].cooltime * (1 - 0.01f * status.Calcool);
            if (cooltimer >= cooltime)
            {
                cooltimer = 0;
                coolimg.enabled = false;
                cooltxt.enabled = false;
                IsSkill = false;
            }
            else
            {
                coolimg.enabled = true;
                cooltxt.enabled = true;
                coolimg.fillAmount = (cooltime - cooltimer) / cooltime;
                cooltxt.text = ((int)(cooltime - cooltimer)).ToString();
                cooltimer += Time.deltaTime;
                if (cooltimer >= sm.skills[14].sustaintime)
                    PlusCritical = 0;
            }
        }
    }

}
