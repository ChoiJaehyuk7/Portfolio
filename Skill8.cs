using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill8 : MonoBehaviour
{
    public KeyCode SkillCode = KeyCode.None;
    public LayerMask mask;
    [HideInInspector] public PlayerMove pm;
    [HideInInspector] public SkillManager sm;
    [HideInInspector] public PlayerHP ph;
    [HideInInspector] public SkillCasting sc;
    [HideInInspector] public CamShake camshake;
    [HideInInspector] public SoundClip sound;
    public Status status;
    bool IsSkill, isTimer;
    float timer;
    Vector3 temp;
    public GameObject skillPrefab;
    public GameObject rangePrefab;
    public GameObject skillrangePrefab;
    public GameObject worldcanvas;
    public Image coolimg;
    public TextMeshProUGUI cooltxt;
    public Camera cam;
    GameObject rangeObj;
    GameObject skillrangeObj;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Use();
    }

    void Use()
    {
        if (Input.GetKeyDown(SkillCode) && !IsSkill && ph.MP >= sm.skills[8].consumeMp)
        {
            if (!IsSkill)
            {
                rangeObj = Instantiate(rangePrefab, pm.transform.position, Quaternion.Euler(90, 0, 0), worldcanvas.transform);
                skillrangeObj = Instantiate(skillrangePrefab, pm.transform.position, Quaternion.Euler(90, 0, 0), worldcanvas.transform);
            }
            rangeObj.transform.localScale = new Vector3(sm.skills[8].distance, sm.skills[8].distance, sm.skills[8].distance);
            skillrangeObj.transform.localScale = new Vector3(sm.skills[8].explo, sm.skills[8].explo, sm.skills[8].explo);

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
                if (dis <= sm.skills[8].distance * 0.4f)
                {
                    if (skillrangeObj != null)
                        skillrangeObj.transform.position = new Vector3(hit.point.x, hit.point.y + 0.05f, hit.point.z);
                }
                else
                {
                    a = sm.skills[8].distance * 0.4f * (hit.point.x - pm.transform.position.x) / dis + pm.transform.position.x;
                    b = sm.skills[8].distance * 0.4f * (hit.point.z - pm.transform.position.z) / dis + pm.transform.position.z;
                    if (skillrangeObj != null)
                        skillrangeObj.transform.position = new Vector3(a, hit.point.y + 0.05f, b);
                }

                if (skillrangeObj != null)
                    skillrangeObj.transform.Rotate(0, 0, 0.5f);

                if (Input.GetMouseButtonDown(1) && !isTimer)
                {
                    sc.id = 8;          //SkillCasting.cs id 값 저장.
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

        if (sc.GaugeDone[8]) //캐스팅 차징 끝나면
        {
            camshake.Shake(0.4f);
            sound.sounds[10].Play();
            ph.MP -= sm.skills[8].consumeMp;
            GameObject skillObj = Instantiate(skillPrefab, temp, Quaternion.identity);
            skillObj.transform.localScale = new Vector3(0.5f + sm.skills[8].explo * 0.003f, 0.75f + sm.skills[8].explo * 0.004f, 0.3f + sm.skills[8].explo * 0.0015f);
            Destroy(skillObj, sm.skills[8].sustaintime);
            isTimer = true;
            sc.GaugeDone[8] = false;
        }

        if (isTimer)
        {
            float cooltime = sm.skills[8].cooltime * (1 - 0.01f * status.Calcool);

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

                if (timer >= 3)
                    sound.sounds[10].Stop();
            }
        }
    }
}
