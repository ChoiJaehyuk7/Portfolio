using TMPro;
using UnityEngine;
public enum StatusType
{
    MP,
    Dex,
    HP,
    Luck
}

[System.Serializable]
public class StatusMP
{
    public StatusType statustype;
    public float SkillDamage;
    public float MaximumMP;
    public float PenetrateMP;
    public float explosion;
    public float GaugeSpeed;
    public float RecoveryMP;

    public float MpValue;
}

[System.Serializable]
public class StatusDex
{
    public StatusType statustype;
    public float CastingSpeed;
    public float MovingSpeed;
    public float CoolTime;
    public float Evasion;

    public float DexValue;
}

[System.Serializable]
public class StatusHP
{
    public StatusType statustype;
    public float MaximumHP;
    public float Armor;
    public float imunity;
    public float recoveryHP;

    public float HpValue;
}

[System.Serializable]
public class StatusLuck
{
    public StatusType statustype;
    public float criticalpercent;
    public float criticaldamage;
    public float acquireExp;
    public float equipmentpercent;

    public float LuckValue;
}

[System.Serializable]
public class StatusInformation
{
    public int Level;
    public int LevelOfJob;
    public string JobName;
    public float CurrentExp;
    public float Currentpercent;
}

[System.Serializable]
public class Status : MonoBehaviour
{
    public StatusMP statusMp;
    public StatusHP statusHp;
    public StatusDex statusDex;
    public StatusLuck statusLuck;
    public StatusInformation infor;

    [HideInInspector] public ExpCointroller EC;
    [HideInInspector] public AbilityController AC;

    [HideInInspector] public bool IsMp, IsHp, IsDex, IsLuck;

    public TextMeshProUGUI TextMp, TextHp, TextDex, TextLuck;
    public TextMeshProUGUI Level, LevelOfJob, Jobname, CurrExp, Exppercent;

    public float Caldamage, Caldamage2, Calpenet, CalmaxMp, Calexplo, Calexplo2, Calgauge, CalrecoveryMP;
    [HideInInspector] public float Calcasting, Calspeed, Calevasion, Calcool;
    public float CalmaxHp, Calarmor, Calimunity, Calrecovery;
    [HideInInspector] public float Calcriticaldamage, Calcritical, Calgetexp, Calgetequip;

    [HideInInspector] public float ssmaxMp, sspenet, ssrecoveryMp;
    [HideInInspector] public float ssmaxHp, ssarmor, ssrecoveryHp;
    private void Start()
    {
        //MP Status Init
        statusMp.SkillDamage = 5;
        statusMp.MaximumMP = 200;
        statusMp.PenetrateMP = 5;
        statusMp.explosion = 5;
        statusMp.GaugeSpeed = 5;
        statusMp.RecoveryMP = 7.5f;
        statusMp.MpValue = 5;

        //HP Status Init
        statusHp.MaximumHP = 100;
        statusHp.Armor = 2;
        statusHp.imunity = 2;
        statusHp.recoveryHP = 2;
        statusHp.HpValue = 2;

        //Dex Status Init
        statusDex.CastingSpeed = 3;
        statusDex.MovingSpeed = 3;
        statusDex.Evasion = 3;
        statusDex.CoolTime = 3;
        statusDex.DexValue = 3;

        //Luck Status Init
        statusLuck.criticaldamage = 1;
        statusLuck.criticalpercent = 1;
        statusLuck.acquireExp = 1;
        statusLuck.equipmentpercent = 1;
        statusLuck.LuckValue = 1;

        //information Init;
        infor.Level = EC.Level;
        infor.LevelOfJob = EC.circle;
        infor.JobName = EC.Jobname[infor.LevelOfJob - 1];
        infor.CurrentExp = EC.currfame;
        infor.Currentpercent = EC.currfamepercent;
        

    }

    private void Update()
    {
        UpStatus();
        StatusTextUI();
        CalculationStatus();
    }

    void UpStatus()
    {
        if (IsMp)
        {
            statusMp.MaximumMP += 50;
            statusMp.PenetrateMP += 1;
            statusMp.SkillDamage += 1;
            statusMp.GaugeSpeed += 1;
            statusMp.explosion += 1;
            statusMp.RecoveryMP += 1;
            statusMp.MpValue += 1;
            IsMp = false;
        }

        if (IsHp)
        {
            statusHp.HpValue += 1;
            statusHp.MaximumHP += 50;
            statusHp.Armor += 1;
            statusHp.imunity += 1;
            statusHp.recoveryHP += 1;
            IsHp = false;
        }

        if (IsDex)
        {
            statusDex.DexValue += 1;
            statusDex.MovingSpeed += 1;
            statusDex.CastingSpeed += 1;
            statusDex.Evasion += 1;
            statusDex.CoolTime += 1;
            IsDex = false;
        }

        if(IsLuck)
        {
            statusLuck.LuckValue += 1;
            statusLuck.criticaldamage += 1;
            statusLuck.criticalpercent += 1;
            statusLuck.equipmentpercent += 1;
            statusLuck.acquireExp += 1;
            IsLuck = false;
        }

        
            infor.Level = EC.Level;
            infor.LevelOfJob = EC.circle;
            infor.JobName = EC.Jobname[infor.LevelOfJob - 1];
        float a, b;

        a = EC.currfame;
        b = EC.currfamepercent;

        infor.CurrentExp = a;
        infor.Currentpercent = 0.01f * b;

    }

    void StatusTextUI()
    {
        if (AC.activeStatus)
        {
            TextMp.text = statusMp.MpValue.ToString();
            TextHp.text = statusHp.HpValue.ToString();
            TextDex.text = statusDex.DexValue.ToString();
            TextLuck.text = statusLuck.LuckValue.ToString();

            Level.text = infor.Level.ToString();
            LevelOfJob.text = infor.LevelOfJob.ToString();
            Jobname.text = infor.JobName.ToString();
            CurrExp.text = infor.CurrentExp.ToString("F1");
            Exppercent.text = infor.Currentpercent.ToString("(0%)");
        }
    }

    void CalculationStatus()
    {

        //magic calculate
        float rand = Random.Range(-2 * statusMp.SkillDamage, 2 * statusMp.SkillDamage);
        Caldamage = 0.5f * statusMp.SkillDamage * statusMp.SkillDamage + 50 + rand;
        Caldamage2 = 0.5f * statusMp.SkillDamage * statusMp.SkillDamage + 50;
        Calpenet = 1.5f * statusMp.PenetrateMP + sspenet;
        CalmaxMp = statusMp.MaximumMP + ssmaxMp;
        Calexplo = 0.15f * statusMp.explosion * statusMp.explosion;
        Calexplo2 = 0.01f * Calexplo;
        Calgauge = statusMp.GaugeSpeed;
        CalrecoveryMP = 1.5f * statusMp.RecoveryMP + ssrecoveryMp;

        //dex calculate
        Calcasting = 2 * statusDex.CastingSpeed;
        Calspeed = 6 * (1 + 0.02f * statusDex.MovingSpeed);
        Calevasion = 0.04f * statusDex.Evasion * statusDex.Evasion;
        Calcool = 1.4f * statusDex.CoolTime;

        //hp calculate
        CalmaxHp = 200 + statusHp.MaximumHP + ssmaxHp;
        Calarmor = 2 * statusHp.Armor + ssarmor;
        Calimunity = statusHp.imunity;
        Calrecovery = 2.5f + (0.5f * statusHp.recoveryHP) + ssrecoveryHp;

        //luck calculate
        Calcritical = 2.5f * statusLuck.criticalpercent - 1;
        Calcriticaldamage = 200 + (0.75f * Calcritical);
        Calgetexp = 1.5f * statusLuck.acquireExp;
        Calgetequip = 0.75f * statusLuck.equipmentpercent;
    }

}