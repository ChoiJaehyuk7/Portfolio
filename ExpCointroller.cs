using UnityEngine;
using UnityEngine.UI;

public class ExpCointroller : MonoBehaviour
{
    public Image ExpBar;
    [HideInInspector] public SkillManager sm;
    [HideInInspector] public AbilityController ac;
    [HideInInspector] public CommandCenter cc;
    public GameObject LvUpEffectprefab;
    public float ExpValue;
    float[] MaxVal = { 0, 50, 80, 130, 200, 300, 410, 550, 700, 860, 1000, 1200, 1450, 1800, 2200, 2700, 3200, 3800, 4500, 5200, 6000 };
    public string[] Jobname = {"뉴 먼", "엘레먼", "매지션", "베테랑", "엘리트", "소서러", "샤 먼", "머 피", "타나토스", "리 치" };
    public float currentval;
    public int Level;
    public int circle;
    public float max;
    public float fame;
    public float currfame;
    public float currfamepercent;
    public bool IsExp;
    public Text LevelText;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        ExpBar.GetComponent<Image>().fillAmount = 0;
        player = GameObject.Find("Character");
        Level = 1;
        circle = 1;
        max = 1.0f / MaxVal[Level];
        fame = MaxVal[Level] + MaxVal[Level + 1];
        currfame = 0;
        currfamepercent = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsExp)
        {
            float temp;
            float CurrentExp;
            temp = currentval;
            CurrentExp = ExpValue;

            if (currfame >= fame)
            {
                currfame = currfame - fame;
                currfamepercent = 100 * currfame / fame;
            }
            else
            {
                currfame += temp;
                currfamepercent = 100 * currfame / fame;
            }
            
            if (CurrentExp + temp >= MaxVal[Level])
            {
                while (CurrentExp + temp >= MaxVal[Level])
                {
                    CurrentExp = CurrentExp + temp - MaxVal[Level];
                    sm.skillpoint += 120;
                    ac.remainvalue += 2;
                    GameObject effectObj = Instantiate(LvUpEffectprefab, player.transform.position, Quaternion.identity);
                    Destroy(effectObj, 1);
                    cc.LevelUpText("레벨 업");
                    Level += 1;
                    LevelText.text = Level.ToString();
                    max = 1 / MaxVal[Level];
                    if (Level % 2 == 1)
                    {
                        circle += 1;
                        fame = MaxVal[Level] + MaxVal[Level + 1];
                    }
                    temp = 0;
                }
                ExpBar.GetComponent<Image>().fillAmount = CurrentExp * max;
                LevelText.text = Level.ToString();
                currentval = 0;
                ExpValue = CurrentExp;
                IsExp = false;
            }
            else
            {
                ExpBar.GetComponent<Image>().fillAmount = (CurrentExp + temp) * max;
                LevelText.text = Level.ToString();
                ExpValue = CurrentExp + temp;
                currentval = 0;
                IsExp = false;
            }
        }
    }
}
