using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [HideInInspector] public DialogueController dialogue;
    [HideInInspector] public SkillCasting sc;
    public CreateEnemy ce;
    //버섯 관련 파라미터
    bool isMushTrigger;
    bool isgauge;
    public bool isQuest1;
    public bool isBuff;
    public bool istriggerText;
    public int MushroomValue;
    GameObject temp;
    public bool one, two, thr, four, five, six, sev;
    [HideInInspector] public bool region1, region2, region3, region4, region5, region6;
    private void Update()
    {
        MushroomController();
    }

    void MushroomController()
    {
        if (isQuest1)
        {
            if (isMushTrigger)
            {
                if (Input.GetKeyDown(KeyCode.V))
                {
                    sc.GaugeUI.SetActive(true);
                    isgauge = true;
                }

                if (isgauge)
                {
                    dialogue.guidePanel.SetActive(false);
                    if (sc.gauge.fillAmount < 1)
                        sc.gauge.fillAmount += Time.deltaTime / 1;
                    else
                    {
                        Destroy(temp.gameObject, 0);
                        MushroomValue += 1;
                        sc.GaugeUI.SetActive(false);
                        temp = null;
                        sc.gauge.fillAmount = 0;
                        isMushTrigger = false;
                        isgauge = false;
                    }
                }
                else
                {
                    dialogue.guidePanel.SetActive(true);
                }
            }
            else
            {
                if (sc.IsGauge)
                {
                    sc.GaugeUI.SetActive(true);
                }
                else
                {
                    sc.GaugeUI.SetActive(false);
                    sc.gauge.fillAmount = 0;
                }
                dialogue.guidePanel.SetActive(false);
                isgauge = false;
                temp = null;
            }

            if (MushroomValue == 5) 
            {
                dialogue.sequence += 1;
                dialogue.synario[dialogue.sequence] = true;
                isQuest1 = false;
            }
        }
    }

    void RegionBoolean(bool a, bool b, bool c, bool d, bool e, bool f, bool g)
    {
        one = a;
        two = b;
        thr = c;
        four = d;
        five = e;
        six = f;
        sev = g;

        istriggerText = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("rb0")) RegionBoolean(true, false, false, false, false, false, false);
        if (other.gameObject.CompareTag("rb1")) RegionBoolean(false, true, false, false, false, false, false);
        if (other.gameObject.CompareTag("rb2")) RegionBoolean(false, false, true, false, false, false, false);
        if (other.gameObject.CompareTag("rb3")) RegionBoolean(false, false, false, true, false, false, false);
        if (other.gameObject.CompareTag("rb4")) RegionBoolean(false, false, false, false, true, false, false);
        if (other.gameObject.CompareTag("rb5")) RegionBoolean(false, false, false, false, false, true, false);
        if (other.gameObject.CompareTag("rb6")) RegionBoolean(false, false, false, false, false, false, true);

        if (other.gameObject.CompareTag("NPC"))
        {
            dialogue.guidePanel.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Shroom"))
        {
            isMushTrigger = true;
            temp = other.gameObject;
        }

        if (other.gameObject.CompareTag("Buff1"))
        {
            isBuff = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Shroom"))
        {
            isMushTrigger = false;
            dialogue.guidePanel.SetActive(false);
            temp = null;
        }
        if (other.gameObject.CompareTag("NPC"))
        {
            dialogue.guidePanel.SetActive(false);
        }

        if (other.gameObject.CompareTag("Buff1"))
        {
            isBuff = false;
        }
    }
}
