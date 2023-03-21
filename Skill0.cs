using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill0 : MonoBehaviour
{
    public KeyCode SkillCode = KeyCode.None;
    public LayerMask mask;
    public GameObject Fireball;
    Transform pos;
    bool IsSkill;
    bool Isdirec;
    GameObject Obj;
    Vector3 InitVec;
    Vector3 a, b;
    public Image coolimg;
    public TextMeshProUGUI cooltxt;
    float cooltimer;
    [HideInInspector] public SkillManager sm;
    [HideInInspector] public Status status;
    [HideInInspector] public PlayerHP ph;
    [HideInInspector] public PlayerMove pm;
    [HideInInspector] public SoundClip sound;
    void Start()
    {
        pos = GameObject.Find("BulletPos").transform;
        InitVec = new Vector3(1, 1, 1);
    }
    void Update()
    {
        Use();
    }

    void Use()
    {
        if (Input.GetKeyDown(SkillCode) && !IsSkill && ph.MP >= sm.skills[0].consumeMp)//스킬코드를 누르고 스킬 사용중이 아니며 스킬 사용가능한 마나가 있을 때
        {
            ph.MP -= sm.skills[0].consumeMp; 
            float incScale = status.Calexplo2 + sm.skills[0].explo * 0.01f;
            Fireball.transform.localScale = new Vector3(InitVec.x + incScale, InitVec.y + incScale, InitVec.z + incScale);
            Obj = Instantiate(Fireball, pos.position, Quaternion.identity);
            Destroy(Obj, sm.skills[0].sustaintime);
            sound.sounds[3].Play();
            IsSkill = true;
            Isdirec = true;
        }

        if (IsSkill)
        {
            if (Obj != null)
                Obj.transform.Translate(direction() * 0.2f);

            float cooltime = sm.skills[0].cooltime * (1 - 0.01f * status.Calcool);

            if(cooltimer >= cooltime)
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
            }
        }
    }

    Vector3 direction()
    {
        if (Isdirec)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, mask))
            {
                pm.agent.enabled = false;
                pm.transform.LookAt(hit.point);
                b = pm.transform.forward;
                pm.agent.speed = 0;
                pm.agent.acceleration = 0;
                Isdirec = false;
            }
        }
        return b;
    }
}
