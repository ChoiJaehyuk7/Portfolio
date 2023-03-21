using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill12 : MonoBehaviour
{
    public KeyCode SkillCode = KeyCode.None;
    public GameObject Fireball;
    bool IsSkill;
    GameObject Obj;

    public Image coolimg;
    public TextMeshProUGUI cooltxt;
    float cooltimer;
    [HideInInspector] public SkillManager sm;
    [HideInInspector] public Status status;
    [HideInInspector] public PlayerHP ph;
    [HideInInspector] public PlayerMove pm;
    [HideInInspector] public PlayerTrigger pt;
    void Update()
    {
        Use();
    }

    void Use()
    {
        if (Input.GetKeyDown(SkillCode) && !IsSkill && ph.MP >= sm.skills[0].consumeMp)//��ų�ڵ带 ������ ��ų ������� �ƴϸ� ��ų ��밡���� ������ ���� ��
        {
            ph.MP -= sm.skills[12].consumeMp;
            float incScale = status.Calexplo2 + sm.skills[12].explo * 0.01f;
            Fireball.transform.localScale = new Vector3(sm.skills[12].explo + incScale, sm.skills[12].explo + incScale, sm.skills[12].explo + incScale);
            Obj = Instantiate(Fireball, pm.transform.position, Quaternion.identity);
            Destroy(Obj, sm.skills[12].sustaintime);

            IsSkill = true;
        }

        if (IsSkill)
        {
            float cooltime = sm.skills[12].cooltime * (1 - 0.01f * status.Calcool);
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
                if (cooltimer >= sm.skills[12].sustaintime)
                    pt.isBuff = false;
            }
        }
    }

}
