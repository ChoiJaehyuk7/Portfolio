using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill4 : MonoBehaviour
{
    public KeyCode SkillCode = KeyCode.None;
    public LayerMask mask;
    public GameObject Fireball;
    public Image coolimg;
    public TextMeshProUGUI cooltxt;
    Transform pos;
    bool IsSkill;
    bool Isdirec;
    GameObject Obj;
    Vector3 tempvec;
    Vector3 InitVec;
    public Camera cam;
    float cooltimer;
    [HideInInspector] public SkillManager sm;
    [HideInInspector] public Status status;
    [HideInInspector] public PlayerHP ph;
    [HideInInspector] public PlayerMove pm;
    public GameObject ff;
    [HideInInspector] public SoundClip sound;
    void Start()
    {
        pos = GameObject.Find("BulletPos").transform;
        InitVec = new Vector3(5, 5, 5);
    }
    // Update is called once per frame
    void Update()
    {
        Use();
    }

    void Use()
    {
        if (Input.GetKeyDown(SkillCode) && !IsSkill && ph.MP >= sm.skills[4].consumeMp)
        {
            ph.MP -= sm.skills[4].consumeMp;
            float incScale = status.Calexplo2 + sm.skills[4].explo * 0.01f;

            Fireball.transform.localScale = new Vector3(
                InitVec.x + incScale,
                InitVec.y + incScale,
                InitVec.z + incScale);
            Obj = Instantiate(Fireball, pos.position, Quaternion.identity);
            Destroy(Obj, sm.skills[4].sustaintime);
            sound.sounds[6].Play();
            IsSkill = true;
            Isdirec = true;
        }

        if (IsSkill)
        {
            if (Obj != null)
                Obj.transform.Translate(direction() * 0.05f);

            float cooltime = sm.skills[4].cooltime * (1 - 0.01f * status.Calcool);

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
            }
        }
    }

    Vector3 direction()
    {
        if (Isdirec)
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100, mask))
            {
                pm.agent.enabled = false;

                pm.transform.LookAt(hit.point);
                tempvec = pm.transform.forward;
                pm.agent.speed = 0;
                pm.agent.acceleration = 0;
                Isdirec = false;
            }
        }

        return tempvec;
    }
}
