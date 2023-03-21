using UnityEngine;
using TMPro;

public class SkillStatus : MonoBehaviour
{
    [HideInInspector] public SkillManager sm;
    [HideInInspector] public Status status;
    [HideInInspector] public SoundClip sound;
    public int ssID;

    public TextMeshProUGUI CurrentLvText, CoolTimeText, ExploText, SpendMpText, SkillPointText;

    private void Start()
    {
        sm.skillpoint = 100;
    }

    public void PressUp()
    {
        sound.sounds[2].Play();
        switch (ssID)
        {
            case 0:
                StatusUpController(6, 5, 1, 0.2f, 1);
                break;
            case 1:
                StatusUpController(12, 7, 1.5f, 0.3f, 1);
                break;
            case 2:
                HealthPassiveUp(0.5f, 15, 0.5f);
                break;
            case 3:
                StatusUpController(10, 13, 0.5f, 0.4f, 1);
                break;
            case 4:
                StatusUpController(15, 11, 1f, 0.4f, 2);
                break;
            case 5:
                ManaPassiveUp(1, 30, 1.5f);
                break;
            case 6:
                StatusUpController(20, 14, 1.2f, 0.4f, 2);
                break;
            case 8:
                StatusUpController(23, 16, 1.2f, 0.5f, 2);
                break;
            case 9:
                StatusUpController(24, 18, 1.4f, 0.6f, 1);
                break;
            case 10:
                StatusUpController(26, 28, 1.4f, 0.6f, 1);
                break;
            case 11:
                StatusUpController(30, 18, 1.4f, 0.6f, 1);
                break;
            case 13:
                StatusUpController(34, 20, 1f, 0.4f, 1);
                break;
            case 15:
                StatusUpController(35, 22, 1.2f, 0.5f, 1);
                break;
        }
        
    }

    public void PressDown()
    {
        sound.sounds[2].Play();
        switch (ssID)
        {
            case 0:
                StatusDownController(6, 5, 1, 0.2f, 1);
                break;
            case 1:
                StatusDownController(12, 7, 1.5f, 0.3f, 1);
                break;
            case 2:
                HealthPassiveDown(0.5f, 15, 0.5f);
                break;
            case 3:
                StatusDownController(10, 13, 0.5f, 0.4f, 1);
                break;
            case 4:
                StatusDownController(15, 11, 1f, 0.4f, 2);
                break;
            case 5:
                ManaPassiveDown(1, 30, 1.5f);
                break;
            case 6:
                StatusDownController(20, 14, 1.2f, 0.4f, 2);
                break;
            case 8:
                StatusDownController(23, 16, 1.2f, 0.5f, 2);
                break;
            case 9:
                StatusDownController(24, 18, 1.4f, 0.6f, 1);
                break;
            case 10:
                StatusDownController(26, 28, 1.4f, 0.6f, 1);
                break;
            case 11:
                StatusDownController(30, 18, 1.4f, 0.6f, 1);
                break;
            case 13:
                StatusDownController(34, 20, 1f, 0.4f, 1);
                break;
            case 15:
                StatusDownController(35, 22, 1.2f, 0.5f, 1);
                break;
        }
    }

    void StatusUpController(int spendMp, int damage, float Explo, float cooltime, int effectscale)
    {
        if (sm.skills[ssID].CurrLv >= 1 && sm.skills[ssID].CurrLv < sm.skills[ssID].maxlv && sm.skillpoint >= sm.skills[ssID].sp)
        {
            sm.skills[ssID].CurrLv += 1;
            sm.skills[ssID].consumeMp += spendMp;
            sm.skills[ssID].damage += damage;
            sm.skills[ssID].explo += Explo;
            sm.skills[ssID].cooltime -= cooltime;
            sm.skills[ssID].effect += effectscale;
            sm.skillpoint -= sm.skills[ssID].sp;

            CurrentLvText.text = sm.skills[ssID].CurrLv.ToString();
            SpendMpText.text = sm.skills[ssID].consumeMp.ToString();
            ExploText.text = sm.skills[ssID].explo.ToString("F1");
            CoolTimeText.text = sm.skills[ssID].cooltime.ToString("F1");
            SkillPointText.text = sm.skillpoint.ToString();
        }  
    }

    void StatusDownController(int spendMp, int damage, float Explo, float cooltime, int effectscale)
    {
        if (sm.skills[ssID].CurrLv > 1)
        {
            sm.skills[ssID].CurrLv -= 1;
            sm.skills[ssID].consumeMp -= spendMp;
            sm.skills[ssID].damage -= damage;
            sm.skills[ssID].explo -= Explo;
            sm.skills[ssID].cooltime += cooltime;
            sm.skills[ssID].effect -= effectscale;
            sm.skillpoint += sm.skills[ssID].sp;

            CurrentLvText.text = sm.skills[ssID].CurrLv.ToString();
            SpendMpText.text = sm.skills[ssID].consumeMp.ToString();
            ExploText.text = sm.skills[ssID].explo.ToString("F1");
            CoolTimeText.text = sm.skills[ssID].cooltime.ToString("F1");
            SkillPointText.text = sm.skillpoint.ToString();
        }
    }

    void HealthPassiveUp(float armor, int maxHp, float recoveryHp)
    {
        if (sm.skills[ssID].CurrLv >= 1 && sm.skills[ssID].CurrLv < sm.skills[ssID].maxlv && sm.skillpoint >= sm.skills[ssID].sp)
        { 
            sm.skills[ssID].CurrLv += 1;
            status.ssarmor += armor;
            status.ssmaxHp += maxHp;
            status.ssrecoveryHp += recoveryHp;
            sm.skillpoint -= sm.skills[ssID].sp;

            CurrentLvText.text = sm.skills[ssID].CurrLv.ToString();
            SkillPointText.text = sm.skillpoint.ToString();
        }
    }

    void HealthPassiveDown(float armor, int maxHp, float recoveryHp)
    {
        if (sm.skills[ssID].CurrLv > 1)
        {
            sm.skills[ssID].CurrLv -= 1;
            status.ssarmor -= armor;
            status.ssmaxHp -= maxHp;
            status.ssrecoveryHp -= recoveryHp;
            sm.skillpoint += sm.skills[ssID].sp;

            CurrentLvText.text = sm.skills[ssID].CurrLv.ToString();
            SkillPointText.text = sm.skillpoint.ToString();
        }
    }

    void ManaPassiveUp(float penet, int maxMp, float recoveryMp)
    {
        if (sm.skills[ssID].CurrLv >= 1 && sm.skills[ssID].CurrLv < sm.skills[ssID].maxlv && sm.skillpoint >= sm.skills[ssID].sp)
        {
            sm.skills[ssID].CurrLv += 1;
            status.sspenet += penet;
            status.ssmaxMp += maxMp;
            status.ssrecoveryMp += recoveryMp;
            sm.skillpoint -= sm.skills[ssID].sp;

            CurrentLvText.text = sm.skills[ssID].CurrLv.ToString();
            SkillPointText.text = sm.skillpoint.ToString();
        }
    }

    void ManaPassiveDown(float penet, int maxMp, float recoveryMp)
    {
        if (sm.skills[ssID].CurrLv > 1)
        {
            sm.skills[ssID].CurrLv -= 1;
            status.sspenet -= penet;
            status.ssmaxMp -= maxMp;
            status.ssrecoveryMp -= recoveryMp;
            sm.skillpoint += sm.skills[ssID].sp;

            CurrentLvText.text = sm.skills[ssID].CurrLv.ToString();
            SkillPointText.text = sm.skillpoint.ToString();
        }
    }
}
