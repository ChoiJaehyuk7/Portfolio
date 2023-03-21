using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public enum SkillType
{
    Deal,
    Buff,
    Passive
}

public class Skill : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int ID;
    public int skillSlotID;
    public string Name;
    public SkillType type;  //스킬 유형
    public Image skillsprite;
    public int CurrLv;
    public int Acqulv;     //제한 레벨
    public int sp;          //소모 sp
    public int maxlv;       //최대 레벨
    public int leadingskillLv;  //선행스킬 레벨
    public string leadingskill; //선행스킬
    [TextArea]
    public string infotext; //스킬 설명
    public float cooltime;
    public int damage;
    public float distance;
    public float castingtime;
    public float explo;
    public float sustaintime;
    public float consumeMp;
    public float skillGaugeValue;
    public float effect;
    //public GameObject EffectSSprefab;
    public Transform canvas;
    public Image img;
    Transform obj;
    Transform previousparent;

    [HideInInspector] public SkillManager sm;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (sm.skills[ID].type != SkillType.Passive && sm.skillButtons[ID].GetComponent<Button>().interactable) 
        {
            previousparent = transform.parent;
            sm.tempsprite = skillsprite.sprite;
            transform.SetParent(canvas);
            transform.SetAsLastSibling();
            obj = Instantiate(img.transform, eventData.position, Quaternion.identity, canvas.transform);
            sm.skillid = ID;

            sm.IsSlot = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (sm.skills[ID].type != SkillType.Passive && sm.skillButtons[ID].GetComponent<Button>().interactable)
        {
            obj.transform.position = eventData.position;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (sm.skills[ID].type != SkillType.Passive && sm.skillButtons[ID].GetComponent<Button>().interactable)
        {
            transform.SetParent(previousparent);
            Destroy(obj.transform.gameObject);
        }
    }
}
