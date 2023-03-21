using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHP : MonoBehaviour
{
    public int enemyID;
    public Image HpImg;
    public Image HpEffectImg;
    public GameObject Gauge;
    public float Hp;
    public float armor;
    public float CCdamaged;
    public float SSdamaged;
    public float GaugeY;
    [SerializeField] private float MaxHp;
    public GameObject fieldItemPrefab;
    List<Item> MummyDB = new List<Item>();
    CommandCenter cc;
    //CreateEnemy ce;
    public bool IsDamaged;
    public bool IsDead;
    public bool IsDeada;
    void Start()
    {
        IsDead = false;
        cc = GameObject.FindWithTag("CommandCenter").GetComponent<CommandCenter>();
        //ce = GameObject.FindWithTag("CreateEnemy").GetComponent<CreateEnemy>();
        Hp = MaxHp;
        armor = 1;
        Gauge.transform.rotation = Quaternion.Euler(-35, -90, 0);
        //ce.EnemyList.Add(this.gameObject);
        for (int i = 0; i < GameObject.Find("ItemDB").GetComponent<ItemDB>().itemDB.Count; i++)
        {
            MummyDB.Add(GameObject.Find("ItemDB").GetComponent<ItemDB>().itemDB[i]);
        }
    }
    void Update()
    {
        if (HpImg.fillAmount > 0)
            HpControl();
    }
    void HpControl()
    {
        Gauge.transform.position = new Vector3(this.gameObject.transform.position.x, 
            this.gameObject.transform.position.y + GaugeY,
            this.gameObject.transform.position.z);
        //Gauge.transform.rotation = Quaternion.Euler(-35, -90, 0);

        if (cc.dmgCC != 0 || cc.dmgSs != 0)
        {
            CCdamaged = Random.Range(cc.dmgCC * 0.8f, cc.dmgCC * 1.2f);
            SSdamaged = Random.Range(cc.dmgSs * 0.8f, cc.dmgSs * 1.2f);
        }

        HpImg.fillAmount = Hp / MaxHp;
        if(HpEffectImg.fillAmount > HpImg.fillAmount)   
            HpEffectImg.fillAmount -= 0.0025f;
        else
            HpEffectImg.fillAmount = HpImg.fillAmount;

        if(HpImg.fillAmount <= 0 && !IsDead)
        {
            //ce.EnemyList.Remove(this.gameObject);
            IsDeada = true;
            int rd = Random.Range(1, 4);
            for (int i = 0; i <  rd; i++)
            {
                GameObject go = Instantiate(fieldItemPrefab, new Vector3(transform.position.x + Random.Range(-3.0f, 3.0f),
                    transform.position.y + 0.5f, transform.position.z + Random.Range(-3.0f, 3.0f)), Quaternion.Euler(-35, -90,0));
                go.GetComponent<FieldItems>().SetItem(perAcquire());
                
                if (i == rd - 1)
                    IsDead = true;
            }
        }
    }

    Item perAcquire() //ÇÊµå¾ÆÀÌÅÛ Àåºñ ¾ÆÀÌÅÛ È¹µæ È®·ü Á¶Á¤
    {
        int rand;

        if(enemyID == 0)
        {
            int a = Random.Range(0, 100);

            if (a > 15)
                rand = Random.Range(0, 2);
            else
                rand = 2;
        }
        else
        {
            rand = Random.Range(enemyID * 3, enemyID * 3 + 3);
        }

        return MummyDB[rand];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Skill"))
        {
            SkillDamage sd = other.gameObject.GetComponent<SkillDamage>();
            sd.TriggerObj.Add(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Skill"))
            if (IsDeada)
            {
                SkillDamage sd = other.gameObject.GetComponent<SkillDamage>();
                sd.TriggerObj.Remove(this.gameObject);
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Skill"))
        {
            SkillDamage sd = other.gameObject.GetComponent<SkillDamage>();
            sd.TriggerObj.Remove(this.gameObject);
        }
    }
}
