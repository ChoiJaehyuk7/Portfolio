using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    [HideInInspector] public Image imgHP;
    [HideInInspector] public Image imgMP;

    public float HP;
    public float maxHP;
    public float MP;
    public float maxMP;
    public GameObject DeadUI;
    public bool isDead;
    [HideInInspector] public TextMeshProUGUI hpTxt;
    [HideInInspector] public TextMeshProUGUI mpTxt;
    [HideInInspector] public Status status;
    PlayerMove pm;
    void Start()
    {
        pm = GetComponent<PlayerMove>();
        DeadUI.SetActive(false);
        StartCoroutine(RecoveryHP());
        HP = maxHP;
        MP = maxMP;
    }

    void Update()
    {
        StateHP();
        StateMP();
        Dead();
    }

    void Dead()
    {
        if (imgHP.fillAmount <= 0)
        {
            pm.agent.enabled = false;
            isDead = true;
            DeadUI.SetActive(true);
            HP = 0;
        }
        else
        {
            isDead = false;
        }
    }

    public void revival()
    {
        transform.position = new Vector3(-75, 1, -145);
        imgHP.fillAmount = 1;
        HP = maxHP;
        MP = maxMP;
        DeadUI.SetActive(false);
        pm.agent.enabled = true;
    }

    public void Exit()
    {
        Application.Quit();
    }
    void StateHP()
    {
        maxHP = status.CalmaxHp;
        imgHP.fillAmount = HP / status.CalmaxHp;
        hpTxt.text = ((int)HP).ToString(); 
    }
    void StateMP()
    {
        maxMP = status.CalmaxMp;
        imgMP.fillAmount = MP / status.CalmaxMp;
        mpTxt.text = ((int)MP).ToString();
    }
    IEnumerator RecoveryHP()
    {
        for (int i = 0; i < 10000; i++)
        {
            yield return new WaitForSeconds(1);

            if (!isDead)
            {
                if (HP + status.Calrecovery < status.CalmaxHp)
                    HP += status.Calrecovery;
                else
                    HP = status.CalmaxHp;

                if (MP + status.CalrecoveryMP < status.CalmaxMp)
                    MP += status.CalrecoveryMP;
                else
                    MP = status.CalmaxMp;
            }
        }
    }
}
