using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill11 : MonoBehaviour
{
    public KeyCode SkillCode = KeyCode.None;
    public LayerMask mask;
    [HideInInspector] public PlayerMove pm;
    [HideInInspector] public SkillManager sm;
    [HideInInspector] public PlayerHP ph;
    [HideInInspector] public SkillCasting sc;
    [HideInInspector] public CamShake camshake;
    [HideInInspector] public Status status;
    [HideInInspector] public SoundClip sound;
    bool IsSkill, isTimer;
    float timer;
    Vector3 temp;
    public GameObject skillPrefab;
    public GameObject rangePrefab;
    public GameObject skillrangePrefab;
    public GameObject worldcanvas;
    public Image coolimg;
    public TextMeshProUGUI cooltxt;

    GameObject rangeObj;
    GameObject skillrangeObj;
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        Use();
    }

    void Use()
    {
        if (Input.GetKeyDown(SkillCode) && !IsSkill && ph.MP >= sm.skills[11].consumeMp)
        {
            if (!IsSkill)
            {
                rangeObj = Instantiate(rangePrefab, pm.transform.position, Quaternion.Euler(90, 0, 0), worldcanvas.transform);
                skillrangeObj = Instantiate(skillrangePrefab, pm.transform.position, Quaternion.Euler(90, 0, 0), worldcanvas.transform);
            }
            rangeObj.transform.localScale = new Vector3(sm.skills[11].distance, sm.skills[11].distance, sm.skills[11].distance);
            skillrangeObj.transform.localScale = new Vector3(sm.skills[11].explo, sm.skills[11].explo, sm.skills[11].explo);

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
                if (dis <= sm.skills[11].distance * 0.4f)
                {
                    if (skillrangeObj != null)
                        skillrangeObj.transform.position = new Vector3(hit.point.x, hit.point.y + 0.05f, hit.point.z);
                }
                else
                {
                    a = sm.skills[11].distance * 0.4f * (hit.point.x - pm.transform.position.x) / dis + pm.transform.position.x;
                    b = sm.skills[11].distance * 0.4f * (hit.point.z - pm.transform.position.z) / dis + pm.transform.position.z;
                    if (skillrangeObj != null)
                        skillrangeObj.transform.position = new Vector3(a, hit.point.y + 0.05f, b);
                }

                if (skillrangeObj != null)
                    skillrangeObj.transform.Rotate(0, 0, 0.5f);

                if (Input.GetMouseButtonDown(1) && !isTimer)
                {
                    sc.id = 11;          //SkillCasting.cs id 값 저장.
                    pm.agent.enabled = false;
                    temp = skillrangeObj.transform.position; //스킬 pos temp값에 저장. (좌표 변경되지 않게)
                    pm.transform.LookAt(temp);
                    pm.agent.speed = 0;
                    pm.agent.acceleration = 0;

                    sc.IsGauge = true;  //SkillCasting.cs Gauge Charging

                    Destroy(skillrangeObj, 0);
                    Destroy(rangeObj, 0);
                }

                if (Input.anyKeyDown && !Input.GetKeyDown(SkillCode))
                    IsSkill = false;
            }

            
        }
        else
        {
            sm.IsUse = false;
            Destroy(skillrangeObj, 0);
            Destroy(rangeObj, 0);
        }

        if (sc.GaugeDone[11]) //캐스팅 차징 끝나면
        {
            camshake.Shake(0.5f);
            sound.sounds[12].Play();
            ph.MP -= sm.skills[11].consumeMp;
            GameObject skillObj = Instantiate(skillPrefab, temp, Quaternion.Euler(180,0,0));
            skillObj.transform.localScale = new Vector3(0.2f + sm.skills[11].explo * 0.002f, 0.15f + sm.skills[11].explo * 0.0015f, 0.2f + sm.skills[11].explo * 0.002f);
            Destroy(skillObj, sm.skills[11].sustaintime);
            isTimer = true;
            sc.GaugeDone[11] = false;
        }

        if (isTimer)
        {
            float cooltime = sm.skills[11].cooltime * (1 - 0.01f * status.Calcool);
            if (timer >= cooltime)
            {
                coolimg.enabled = false;
                cooltxt.enabled = false;
                timer = 0;
                isTimer = false;
                IsSkill = false;
            }
            else
            {
                timer += Time.deltaTime;
                coolimg.enabled = true;
                cooltxt.enabled = true;
                cooltxt.text = ((int)(cooltime - timer)).ToString();
                coolimg.fillAmount = (cooltime - timer) / cooltime;

                if (timer > 3)
                    sound.sounds[12].Stop();
            }
        }
    }
}
