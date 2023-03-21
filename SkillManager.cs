using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillManager : MonoBehaviour
{
    [HideInInspector] public ExpCointroller EC;

    public Skill[] skills;
    public SkillButton[] skillButtons;
    public Skill activateSkill;
    public Sprite tempsprite;
    public GameObject Panel;
    public bool actSkill;
    public bool IsSlot = false;
    public bool IsUse;
    public int skillid;
    public int skillpoint;
    public TextMeshProUGUI spTxt;
    public GameObject contentspanel;
    public GameObject Infopanel;
    public GameObject[] lines;
    public SkillSlot[] slots;
    public bool isSlot4, isSlot9, isSlot10, isSlot11, isSlot12, isSlot13, isSlot15;
    public float a;
    private void Start()
    {
        Panel.SetActive(actSkill);

        for(int i = 3; i<30; i++)
            skillButtons[i].GetComponent<Button>().interactable = false;
    }

    private void Update()
    {
        OpenSkillPanel();
        spTxt.text = skillpoint.ToString();
        LineController();
    }
    public void ClickExit()
    {
        Panel.SetActive(false);
        actSkill = false;
    }

    void OpenSkillPanel()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            actSkill = !actSkill;
            Panel.SetActive(actSkill);

            if (actSkill)
            {
                contentspanel.transform.position = new Vector2(contentspanel.transform.position.x, -650);
                Infopanel.transform.position = new Vector2(Infopanel.transform.position.x, 150);
                Panel.transform.SetAsLastSibling();
            }
        }
    }

    void LineController()
    {
        switch (EC.Level)
        {
            case 2:
               skillButtons[3].GetComponent<Button>().interactable = true;
                break;
            case 4:
                skillButtons[6].GetComponent<Button>().interactable = true;
                break;
            case 5:
                skillButtons[8].GetComponent<Button>().interactable = true;
                break;
            case 6:
                skillButtons[7].GetComponent<Button>().interactable = true;
                break;
            case 9:
                skillButtons[14].GetComponent<Button>().interactable = true;
                break;
        }

        if (skills[0].CurrLv >= skills[4].leadingskillLv && EC.Level >= skills[4].Acqulv)
        {
            skillButtons[4].GetComponent<Button>().interactable = true;
            isSlot4 = false;
            lines[0].GetComponent<Image>().color = new Color(0, 1, 0, 1);
        }
        else
        {
            skillButtons[4].GetComponent<Button>().interactable = false;
            lines[0].GetComponent<Image>().color = new Color(0, 0, 0, 0.39f);

            int a = skills[4].CurrLv - 1;

            slots[skills[4].skillSlotID].GetComponent<Image>().sprite = null;
            isSlot4 = true;
            if (a != 0)
            {
                skills[4].CurrLv = 1;
                skillpoint += skills[4].sp * a;
                a = 0;
            }
        }

        if (skills[2].CurrLv >= skills[5].leadingskillLv && EC.Level >= skills[5].Acqulv)
        {
            skillButtons[5].GetComponent<Button>().interactable = true;
            lines[1].GetComponent<Image>().color = new Color(0, 1, 0, 1);
        }
        else
        {
            skillButtons[5].GetComponent<Button>().interactable = false;
            lines[1].GetComponent<Image>().color = new Color(0, 0, 0, 0.39f);

            int a = skills[5].CurrLv - 1;
            if (a != 0)
            {
                skills[5].CurrLv = 1;
                skillpoint += skills[5].sp * a;
                a = 0;
            }
        }

        if (skills[6].CurrLv >= skills[9].leadingskillLv && EC.Level >= skills[9].Acqulv)
        {
            skillButtons[9].GetComponent<Button>().interactable = true;
            isSlot9 = false;
            lines[2].GetComponent<Image>().color = new Color(0, 1, 0, 1);
            lines[3].GetComponent<Image>().color = new Color(0, 1, 0, 1);
        }
        else
        {
            skillButtons[9].GetComponent<Button>().interactable = false;
            lines[2].GetComponent<Image>().color = new Color(0, 0, 0, 0.39f);
            lines[3].GetComponent<Image>().color = new Color(0, 0, 0, 0.39f);
            int a = skills[9].CurrLv - 1;

            slots[skills[9].skillSlotID].GetComponent<Image>().sprite = null;
            isSlot9 = true;
            if (a != 0)
            {
                skills[9].CurrLv = 1;
                skillpoint += skills[9].sp * a;
                a = 0;
            }
        }

        if (skills[7].CurrLv >= skills[10].leadingskillLv && EC.Level >= skills[10].Acqulv)
        {
            skillButtons[10].GetComponent<Button>().interactable = true;
            isSlot9 = false;
            lines[4].GetComponent<Image>().color = new Color(0, 1, 0, 1);
        }
        else
        {
            skillButtons[10].GetComponent<Button>().interactable = false;
            lines[4].GetComponent<Image>().color = new Color(0, 0, 0, 0.39f);
            int a = skills[10].CurrLv - 1;

            slots[skills[10].skillSlotID].GetComponent<Image>().sprite = null;
            isSlot10 = true;
            if (a != 0)
            {
                skills[10].CurrLv = 1;
                skillpoint += skills[10].sp * a;
                a = 0;
            }
        }

        if (skills[8].CurrLv >= skills[11].leadingskillLv && EC.Level >= skills[11].Acqulv)
        {
            skillButtons[11].GetComponent<Button>().interactable = true;
            isSlot11= false;
            lines[5].GetComponent<Image>().color = new Color(0, 1, 0, 1);
        }
        else
        {
            skillButtons[11].GetComponent<Button>().interactable = false;
            lines[5].GetComponent<Image>().color = new Color(0, 0, 0, 0.39f);
            int a = skills[11].CurrLv - 1;

            slots[skills[11].skillSlotID].GetComponent<Image>().sprite = null;
            isSlot11 = true;
            if (a != 0)
            {
                skills[11].CurrLv = 1;
                skillpoint += skills[11].sp * a;
                a = 0;
            }
        }

        if (skills[9].CurrLv >= skills[12].leadingskillLv && EC.Level >= skills[12].Acqulv)
        {
            skillButtons[12].GetComponent<Button>().interactable = true;
            isSlot12 = false;
            lines[6].GetComponent<Image>().color = new Color(0, 1, 0, 1);
        }
        else
        {
            skillButtons[12].GetComponent<Button>().interactable = false;
            lines[6].GetComponent<Image>().color = new Color(0, 0, 0, 0.39f);
            int a = skills[12].CurrLv - 1;

            slots[skills[12].skillSlotID].GetComponent<Image>().sprite = null;
            isSlot12 = true;
            if (a != 0)
            {
                skills[12].CurrLv = 1;
                skillpoint += skills[12].sp * a;
                a = 0;
            }
        }

        if (skills[10].CurrLv >= skills[13].leadingskillLv && EC.Level >= skills[13].Acqulv)
        {
            skillButtons[13].GetComponent<Button>().interactable = true;
            isSlot13 = false;
            lines[8].GetComponent<Image>().color = new Color(0, 1, 0, 1);
        }
        else
        {
            skillButtons[13].GetComponent<Button>().interactable = false;
            lines[8].GetComponent<Image>().color = new Color(0, 0, 0, 0.39f);
            int a = skills[13].CurrLv - 1;

            slots[skills[13].skillSlotID].GetComponent<Image>().sprite = null;
            isSlot13 = true;
            if (a != 0)
            {
                skills[13].CurrLv = 1;
                skillpoint += skills[13].sp * a;
                a = 0;
            }
        }

        if (skills[11].CurrLv >= skills[15].leadingskillLv && EC.Level >= skills[15].Acqulv)
        {
            skillButtons[15].GetComponent<Button>().interactable = true;
            isSlot15 = false;
            lines[7].GetComponent<Image>().color = new Color(0, 1, 0, 1);
        }
        else
        {
            skillButtons[15].GetComponent<Button>().interactable = false;
            lines[7].GetComponent<Image>().color = new Color(0, 0, 0, 0.39f);
            int a = skills[13].CurrLv - 1;

            slots[skills[15].skillSlotID].GetComponent<Image>().sprite = null;
            isSlot15 = true;
            if (a != 0)
            {
                skills[15].CurrLv = 1;
                skillpoint += skills[15].sp * a;
                a = 0;
            }
        }
    }
}
