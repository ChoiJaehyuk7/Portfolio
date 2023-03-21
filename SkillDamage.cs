using UnityEngine;
using System.Collections.Generic;
public class SkillDamage : MonoBehaviour
{
    public int dmgID;
    public GameObject Effectprefab;
    bool isTrigg;
    public List<GameObject> TriggerObj = new List<GameObject>();
    public List<float> dmg = new List<float>();
    SkillGauge sg;
    CommandCenter cc;
    SkillManager sm;
    Status status;
    RandomSeed rs;
    Skill14 s14;
    SoundClip sound;
    float timer;
    float i;
    bool isCC;
    bool isSS;
    private void Start()
    {
        cc = GameObject.FindWithTag("CommandCenter").GetComponent<CommandCenter>();
        sg = GameObject.FindWithTag("SkillManager").GetComponent<SkillGauge>();
        sm = GameObject.FindWithTag("SkillManager").GetComponent<SkillManager>();
        rs = GameObject.FindWithTag("RandomSeed").GetComponent<RandomSeed>();
        status = GameObject.FindWithTag("StatusController").GetComponent<Status>();
        s14 = GameObject.FindWithTag("SkillManager").GetComponent<Skill14>();
        sound = GameObject.FindWithTag("Sound").GetComponent<SoundClip>();
    }

    private void Update()
    {
        a();
        b();
    }

    void a()
    {
        if (isCC)
        {
            for (int i = 0; i < TriggerObj.Count; i++)
            {
                dmg.Add(TriggerObj[i].GetComponent<EnemyHP>().CCdamaged);
                if (status.Calcritical + s14.PlusCritical> rs.percent)
                {
                    dmg[i] = dmg[i] * status.Calcriticaldamage * 0.01f;
                    CriticalDamageText((int)dmg[i], TriggerObj[i].transform.position);
                }
                else
                    DamageText((int)dmg[i], TriggerObj[i].transform.position);
                TriggerObj[i].GetComponent<EnemyHP>().Hp -= (int)dmg[i];
                TriggerObj[i].GetComponent<EnemyHP>().IsDamaged = true;
            }
            cc.dmgCC = 0;
            TriggerObj.Clear();
            dmg.Clear();
            isCC = false;
        }
    }

    void b()
    {
        timer += Time.deltaTime;

        if (cc.dmgSs != 0 && isTrigg && isSS)
        {
            if (timer >= i)
            {
                SSDmg();
                if (i == sm.skills[dmgID].sustaintime - 1)
                {
                    timer = 0;
                    isSS = false;
                    i = 0;
                    cc.dmgSs = 0;
                }
                else
                {
                    switch (dmgID)//Å¸¼ö
                    {
                        case 1: i += 1; break;
                        case 4: i += 0.5f; break;
                        case 6: i += 0.5f; break;
                        case 8: i += 0.5f; break;
                        case 9: i += 1; break;
                        case 11: i += 0.25f; break;
                        case 13: i += 0.25f; break;
                        case 15: i += 0.25f; break;
                    }
                }
            }
        }
    }

    void SSDmg()
    {
        sg.gaugeVal += sm.skills[dmgID].skillGaugeValue * (1 + status.Calgauge * 0.05f);
        //isDmgd = true;
        for (int i = 0; i < TriggerObj.Count; i++)
        {
            if (TriggerObj[i] != null)
                dmg.Add(TriggerObj[i].GetComponent<EnemyHP>().SSdamaged);
            if (status.Calcritical + s14.PlusCritical > rs.percent)
            {
                dmg[i] = dmg[i] * status.Calcriticaldamage * 0.01f;
                CriticalDamageText((int)dmg[i], TriggerObj[i].transform.position);
            }
            else
                DamageText((int)dmg[i], TriggerObj[i].transform.position);
            TriggerObj[i].GetComponent<EnemyHP>().Hp -= (int)dmg[i];
            TriggerObj[i].GetComponent<EnemyHP>().IsDamaged = true;
        }
        dmg.Clear();
    }

    public void DamageText(int dmg, Vector3 vec)
    {
        sound.sounds[13].Play();
        GameObject EffectObj = Instantiate(Effectprefab, vec, Quaternion.identity);
        EffectObj.transform.localScale = new Vector3(sm.skills[dmgID].effect, sm.skills[dmgID].effect, sm.skills[dmgID].effect);
        Destroy(EffectObj, 0.5f);

        GameObject DText = Instantiate(cc.DmgText, new Vector3(
           vec.x,
           vec.y + 3f,
           vec.z), Quaternion.Euler(35, 90, 0));
        DText.GetComponent<DamageTextController>().damage = dmg;
    }

    public void CriticalDamageText(int dmg, Vector3 vec)
    {
        sound.sounds[13].Play();
        GameObject EffectObj = Instantiate(Effectprefab, vec, Quaternion.identity);
        EffectObj.transform.localScale = new Vector3(sm.skills[dmgID].effect, sm.skills[dmgID].effect, sm.skills[dmgID].effect);
        Destroy(EffectObj, 0.5f);

        GameObject DText = Instantiate(cc.CriticalDmgText, new Vector3(
           vec.x,
           vec.y + 3f,
           vec.z), Quaternion.Euler(35, 90, 0));

        DText.GetComponent<DamageTextController>().damage = dmg;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            isTrigg = true;
            switch (dmgID) 
            {
                case 0:
                    isCC = true;
                    sg.gaugeVal += 2 + (2 * status.Calgauge * 0.05f);
                    break;
                case 3:
                    isCC = true;
                    sg.gaugeVal += 2 + (2 * status.Calgauge * 0.05f);
                    break;
                case 10:
                    isCC = true;
                    sg.gaugeVal += 4 + (4 * status.Calgauge * 0.05f);
                    break;
            }

            switch (other.gameObject.tag)
            {
                case "Mummy":
                    cc.dmgCC = (int)((sm.skills[dmgID].damage + status.Caldamage) * (1 + 0.01f * (status.Calpenet - 1)) * (1 + sg.gaugeDmg * 0.3f));
                    break;
                case "BigMummy":
                    cc.dmgCC = (int)((sm.skills[dmgID].damage + status.Caldamage) * (1 + 0.01f * (status.Calpenet - 2.5f)) * (1 + sg.gaugeDmg * 0.3f));
                    break;
                case "Ghoul":
                    cc.dmgCC = (int)((sm.skills[dmgID].damage + status.Caldamage) * (1 + 0.01f * (status.Calpenet - 2.5f)) * (1 + sg.gaugeDmg * 0.3f));
                    break;
                case "Demon":
                    cc.dmgCC = (int)((sm.skills[dmgID].damage + status.Caldamage) * (1 + 0.01f * (status.Calpenet - 4f)) * (1 + sg.gaugeDmg * 0.3f));
                    break;
                case "Golem":
                    cc.dmgCC = (int)((sm.skills[dmgID].damage + status.Caldamage) * (1 + 0.01f * (status.Calpenet - 8)) * (1 + sg.gaugeDmg * 0.3f));
                    break;
                case "Whale":
                    cc.dmgCC = (int)((sm.skills[dmgID].damage + status.Caldamage) * (1 + 0.01f * (status.Calpenet - 10)) * (1 + sg.gaugeDmg * 0.3f));
                    break;
                case "Crab":
                    cc.dmgCC = (int)((sm.skills[dmgID].damage + status.Caldamage) * (1 + 0.01f * (status.Calpenet - 13)) * (1 + sg.gaugeDmg * 0.3f));
                    break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            isTrigg = true;
            switch (dmgID)
            {
                case 1:
                    isSS = true;
                    break;
                case 4:
                    isSS = true;
                    break;
                case 6:
                    isSS = true;
                    break;
                case 8:
                    isSS = true;
                    break;
                case 9:
                    isSS = true;
                    break;
                case 11:
                    isSS = true;
                    break;
                case 13:
                    isSS = true;
                    break;
                case 15:
                    isSS = true;
                    break;
            }

            switch (other.gameObject.tag)
            {
                case "Mummy":
                    cc.dmgSs = (int)(0.5f * (sm.skills[dmgID].damage + status.Caldamage) * (1 + 0.01f * (status.Calpenet - 1)) * (1 + sg.gaugeDmg * 0.3f));
                    break;
                case "BigMummy":
                    cc.dmgSs = (int)(0.5f * (sm.skills[dmgID].damage + status.Caldamage) * (1 + 0.01f * (status.Calpenet - 2.5f)) * (1 + sg.gaugeDmg * 0.3f));
                    break;
                case "Ghoul":
                    cc.dmgSs = (int)(0.5f * (sm.skills[dmgID].damage + status.Caldamage) * (1 + 0.01f * (status.Calpenet - 2.5f)) * (1 + sg.gaugeDmg * 0.3f));
                    break;
                case "Demon":
                    cc.dmgSs = (int)(0.5f * (sm.skills[dmgID].damage + status.Caldamage) * (1 + 0.01f * (status.Calpenet - 4f)) * (1 + sg.gaugeDmg * 0.3f));
                    break;
                case "Golem":
                    cc.dmgSs = (int)(0.5f * (sm.skills[dmgID].damage + status.Caldamage) * (1 + 0.01f * (status.Calpenet - 8)) * (1 + sg.gaugeDmg * 0.3f));
                    break;
                case "Whale":
                    cc.dmgSs = (int)(0.5f * (sm.skills[dmgID].damage + status.Caldamage) * (1 + 0.01f * (status.Calpenet - 10)) * (1 + sg.gaugeDmg * 0.3f));
                    break;
                case "Crab":
                    cc.dmgSs = (int)(0.5f * (sm.skills[dmgID].damage + status.Caldamage) * (1 + 0.01f * (status.Calpenet - 13)) * (1 + sg.gaugeDmg * 0.3f));
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isTrigg = false;
    }
}
