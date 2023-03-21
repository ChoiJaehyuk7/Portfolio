using UnityEngine;
using UnityEngine.UI;

public class SkillCasting : MonoBehaviour
{
    public GameObject GaugeUI;
    [HideInInspector] public Status status;
    public Image gauge;
    public Animator ani;
    public bool IsGauge;
    public bool[] GaugeDone;
    public int id;

    [HideInInspector] public SkillManager sm;
    void Start()
    {
        GaugeUI.SetActive(false);
        gauge.fillAmount = 0;
        ani.GetComponent<Animator>();
    }

    void Update()
    {
        IsCasting();
    }

    void IsCasting()
    {
        if (IsGauge)
        {
            GaugeUI.SetActive(true);
            gauge.fillAmount += Time.deltaTime / (sm.skills[id].castingtime - sm.skills[id].castingtime * status.Calcasting * 0.01f);
            ani.SetBool("Casting", true);
            if (gauge.fillAmount == 1)
            {
                GaugeUI.SetActive(false);
                ani.SetBool("Casting", false);
                gauge.fillAmount = 0;
                GaugeDone[id] = true;
                IsGauge = false;
            }
        }
    }
}
