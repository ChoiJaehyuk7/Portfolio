using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill3 : MonoBehaviour
{
    public KeyCode SkillCode = KeyCode.None;
    public LayerMask mask;
    [HideInInspector] public PlayerMove pm;
    [HideInInspector] public PlayerHP ph;
    [HideInInspector] public SkillManager sm;
    [HideInInspector] public Status status;
    [HideInInspector] public SoundClip sound;
    bool IsSkill, isTimer;
    float cooltimer;
    public GameObject skillPrefab;
    public GameObject rangePrefab;
    public GameObject skillrangePrefab;
    public GameObject worldcanvas;
    public Image coolimg;
    public TextMeshProUGUI cooltxt;
    GameObject skillObj;
    GameObject rangeObj;
    GameObject skillrangeObj;
    public Camera cam;
    // Update is called once per framej
    void Update()
    {
        Use();
    }

    void Use()
    {
        if (Input.GetKeyDown(SkillCode) && !IsSkill && ph.MP >= sm.skills[3].consumeMp)
        {
            if (!IsSkill)
            {
                rangeObj = Instantiate(rangePrefab, pm.transform.position, Quaternion.Euler(90, 0, 0), worldcanvas.transform);
                skillrangeObj = Instantiate(skillrangePrefab, pm.transform.position, Quaternion.Euler(90, 0, 0), worldcanvas.transform);
            }
            rangeObj.transform.localScale = new Vector3(15, 15, 15);
            skillrangeObj.transform.localScale = new Vector3(sm.skills[3].explo, sm.skills[3].explo, sm.skills[3].explo);

            IsSkill = true;
        }
        

        if (IsSkill)
        {
            sm.IsUse = true;

            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100, mask))
            {
                float dis = Vector3.Distance(pm.transform.position, hit.point);
                float a, b;
                if (dis <= sm.skills[3].distance * 0.4f)
                {
                    if (skillrangeObj != null)
                        skillrangeObj.transform.position = new Vector3(hit.point.x, hit.point.y + 0.05f, hit.point.z);
                }
                else
                {
                    a = sm.skills[3].distance * 0.4f * (hit.point.x - pm.transform.position.x) / dis + pm.transform.position.x;
                    b = sm.skills[3].distance * 0.4f * (hit.point.z - pm.transform.position.z) / dis + pm.transform.position.z;
                    if (skillrangeObj != null)
                        skillrangeObj.transform.position = new Vector3(a, hit.point.y + 0.05f, b);
                }

                if (skillrangeObj != null)
                    skillrangeObj.transform.Rotate(0, 0, 0.5f);

                if (Input.GetMouseButtonDown(1) && !isTimer)
                {
                    ph.MP -= sm.skills[3].consumeMp;
                    isTimer = true;
                    sound.sounds[5].Play();
                    skillObj = Instantiate(skillPrefab, skillrangeObj.transform.position, Quaternion.identity);
                    skillObj.transform.localScale = new Vector3(0.2f + sm.skills[3].explo * 0.005f, 0.2f + sm.skills[3].explo * 0.005f, 0.2f + sm.skills[3].explo * 0.005f);
                    Destroy(skillObj, sm.skills[3].sustaintime);
                    Destroy(skillrangeObj, 0);
                    Destroy(rangeObj, 0);
                }
            }

            if (Input.anyKeyDown && !Input.GetKeyDown(SkillCode))
                IsSkill = false;
        }
        else
        {
            sm.IsUse = false;
            Destroy(skillrangeObj, 0);
            Destroy(rangeObj, 0);
        }

        if (isTimer)
        {
            float cooltime = sm.skills[3].cooltime * (1 - 0.01f * status.Calcool);
            if (cooltimer >= cooltime)
            {
                cooltimer = 0;
                coolimg.enabled = false;
                cooltxt.enabled = false;
                isTimer = false;
                IsSkill = false;
            }
            else
            {
                coolimg.enabled = true;
                cooltxt.enabled = true;
                coolimg.fillAmount = (cooltime - cooltimer) / cooltime;
                cooltxt.text = ((int)(cooltime - cooltimer)).ToString();
                cooltimer += Time.deltaTime;

                //Debug.Log(cooltimer);
                //if (cooltimer > 1f)
                //{
                //    Debug.Log("F");
                //    skillPrefab.GetComponent<BoxCollider>().enabled = false;
                //}
            }
        }
    }
}
