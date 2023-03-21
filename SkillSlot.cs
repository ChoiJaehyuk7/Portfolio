using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public KeyCode keycode;
    public int SlotID;
    public bool EndDragOnSlot;
    public Image img;
    public bool AF;
    public Image cooltimeImg;
    public TextMeshProUGUI cooltimeTxt;
    SkillManager sm;
    Skill0 s0;
    Skill1 s1;
    Skill3 s3;
    Skill4 s4;
    Skill6 s6;
    Skill7 s7;
    Skill8 s8;
    Skill9 s9;
    Skill10 s10;
    Skill11 s11;
    Skill12 s12;
    Skill13 s13;
    Skill14 s14;
    Skill15 s15;
    private void Start()
    {
        sm = FindObjectOfType<SkillManager>();
        s0 = FindObjectOfType<Skill0>();
        s1 = FindObjectOfType<Skill1>();
        s3 = FindObjectOfType<Skill3>();
        s4 = FindObjectOfType<Skill4>();
        s6 = FindObjectOfType<Skill6>();
        s7 = FindObjectOfType<Skill7>();
        s8 = FindObjectOfType<Skill8>();
        s9 = FindObjectOfType<Skill9>();
        s10 = FindObjectOfType<Skill10>();
        s11 = FindObjectOfType<Skill11>();
        s12 = FindObjectOfType<Skill12>();
        s13 = FindObjectOfType<Skill13>();
        s14 = FindObjectOfType<Skill14>();
        s15 = FindObjectOfType<Skill15>();
        cooltimeImg.enabled = false;
        cooltimeTxt.enabled = false;
    }

    private void Update()
    {
        if (sm.isSlot4) s4.SkillCode = KeyCode.None;
        if (sm.isSlot9) s9.SkillCode = KeyCode.None;
        if (sm.isSlot10) s10.SkillCode = KeyCode.None;
        if (sm.isSlot11) s11.SkillCode = KeyCode.None;
        if (sm.isSlot12) s12.SkillCode = KeyCode.None;
        if (sm.isSlot13) s13.SkillCode = KeyCode.None;
        if (sm.isSlot15) s15.SkillCode = KeyCode.None;
    }

    public void OnPointerEnter(PointerEventData eventData)
    { 
        AF = true;

        if (AF && sm.IsSlot)
        {
            switch (sm.skillid)
            {
                case 0:
                    s0.SkillCode = keycode;
                    s0.coolimg = cooltimeImg;
                    s0.cooltxt = cooltimeTxt;
                    break;
                case 1:
                    s1.SkillCode = keycode;
                    s1.coolimg = cooltimeImg;
                    s1.cooltxt = cooltimeTxt;
                    break;
                case 3:
                    s3.SkillCode = keycode;
                    s3.coolimg = cooltimeImg;
                    s3.cooltxt = cooltimeTxt;
                    break;
                case 4:
                    s4.SkillCode = keycode;
                    s4.coolimg = cooltimeImg;
                    s4.cooltxt = cooltimeTxt;
                    sm.skills[sm.skillid].skillSlotID = SlotID;
                    break;
                case 6:
                    s6.SkillCode = keycode;
                    s6.coolimg = cooltimeImg;
                    s6.cooltxt = cooltimeTxt;
                    break;
                case 7:
                    s7.SkillCode = keycode;
                    s7.coolimg = cooltimeImg;
                    s7.cooltxt = cooltimeTxt;
                    break;
                case 8:
                    s8.SkillCode = keycode;
                    s8.coolimg = cooltimeImg;
                    s8.cooltxt = cooltimeTxt;
                    break;
                case 9:
                    s9.SkillCode = keycode;
                    s9.coolimg = cooltimeImg;
                    s9.cooltxt = cooltimeTxt;
                    sm.skills[sm.skillid].skillSlotID = SlotID;
                    break;
                case 10:
                    s10.SkillCode = keycode;
                    s10.coolimg = cooltimeImg;
                    s10.cooltxt = cooltimeTxt;
                    sm.skills[sm.skillid].skillSlotID = SlotID;
                    break;
                case 11:
                    s11.SkillCode = keycode;
                    s11.coolimg = cooltimeImg;
                    s11.cooltxt = cooltimeTxt;
                    sm.skills[sm.skillid].skillSlotID = SlotID;
                    break;
                case 12:
                    s12.SkillCode = keycode;
                    s12.coolimg = cooltimeImg;
                    s12.cooltxt = cooltimeTxt;
                    sm.skills[sm.skillid].skillSlotID = SlotID;
                    break;
                case 13:
                    s13.SkillCode = keycode;
                    s13.coolimg = cooltimeImg;
                    s13.cooltxt = cooltimeTxt;
                    sm.skills[sm.skillid].skillSlotID = SlotID;
                    break;
                case 14:
                    s14.SkillCode = keycode;
                    s14.coolimg = cooltimeImg;
                    s14.cooltxt = cooltimeTxt;
                    break;
                case 15:
                    s15.SkillCode = keycode;
                    s15.coolimg = cooltimeImg;
                    s15.cooltxt = cooltimeTxt;
                    sm.skills[sm.skillid].skillSlotID = SlotID;
                    break;
            }
            img.sprite = sm.tempsprite;
            sm.IsSlot = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AF = false;
        sm.IsSlot = false;
    }
}
