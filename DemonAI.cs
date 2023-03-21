using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class DemonAI : MonoBehaviour
{
    NavMeshAgent AIagent;
    Transform Player;
    ExpCointroller EC;
    CreateEnemy ce;
    PlayerHP ph;
    Status status;
    public LayerMask WhatIsPlayer, WhatIsGround;
    public Animator MummyAni;
    public float speed, attackrange, chaserange;
    public int exp;
    public bool IsCreate, IsDead;
    bool isPatrol;
    bool ComboAttack;
    float Distance;
    float direcTimer;
    float AniCreateTime;
    public new Collider collider;
    public Collider collider2;
    Vector3 aiDirection;
    public GameObject miniObj;
    void Start()
    {
        AIagent = GetComponent<NavMeshAgent>();
        AIagent.enabled = false;
        Player = GameObject.Find("Character").transform;
        IsCreate = true;
        isPatrol = true;
        EC = GameObject.FindWithTag("ExpController").GetComponent<ExpCointroller>();
        ce = GameObject.FindWithTag("CreateEnemy").GetComponent<CreateEnemy>();
        ph = GameObject.FindWithTag("Player").GetComponent<PlayerHP>();
        status = GameObject.FindWithTag("StatusController").GetComponent<Status>();
        ce.EnemyList.Add(this.gameObject);
        ComboAttack = true;
        StartCoroutine(enuUpdate());
    }

    IEnumerator enuUpdate()
    {
        while (true)
        {
            Distance = Vector3.Distance(this.transform.position, Player.transform.position);
            yield return new WaitForSeconds(0.5f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        miniObj.transform.position = new Vector3(transform.position.x, 104.5f, transform.position.z);

        Command(); // 积己瞪 锭 Animation

        if (!IsCreate) // 积己等 饶
        {
            if (!ph.isDead)
            {
                if (Distance <= attackrange)
                    Attacking();
                else if (Distance <= chaserange)
                    ChasePlayer();
                else
                    Patrolling();
            }
            else
            {

                Patrolling();
            }
            Attacked();
            DeadAni();
        }
    }

    void Command()
    {
        if (IsCreate)
        {
            AniCreateTime += Time.deltaTime;
            if (AniCreateTime >= 2.2f)
            {
                IsCreate = false;
                collider.enabled = false;
                collider2.isTrigger = true;
                AIagent.enabled = true;
                ClipStateControl(false, false, false, true, false);
                AniCreateTime = 0;
            }
            else
                AniCreateTime += Time.deltaTime;
        }
    }

    Vector3 randPos()
    {
        float posX = transform.position.x + Random.Range(-10, 10);
        float poxZ = transform.position.z + Random.Range(-10, 10);
        return new Vector3(posX, transform.position.y, poxZ);
    }
    void Patrolling()
    {
        ClipStateControl(false, false, false, true, false);
        AIagent.speed = speed;
        ComboAttack = false;
        if (isPatrol)
        {
            aiDirection = randPos();
            isPatrol = false;
        }

        if (direcTimer > 2)
        {
            aiDirection = randPos();
            direcTimer = 0;
        }
        else
            direcTimer += Time.deltaTime;

        if (GetComponent<EnemyHP>().HpImg.fillAmount > 0)
        {
            if (Vector3.Distance(transform.position, aiDirection) <= 2)
            {
                aiDirection = randPos();
                direcTimer = 0;
            }
            else
            {
                AIagent.SetDestination(aiDirection);
            }
        }
    }

    void ChasePlayer()
    {
        if (!IsDead)
        {
            ComboAttack = true;
            ClipStateControl(false, false, false, true, false);
            if (AIagent.speed == 0)
                Invoke("DelaySpeed", 0.5f);
            else
            {
                AIagent.enabled = true;
                AIagent.speed = speed;
            }

            if (AIagent.enabled)
                AIagent.SetDestination(Player.position);
        }
    }

    void Attacking()
    {
        AttackAni();
        AIagent.speed = 0;
        if (GetComponent<EnemyHP>().HpImg.fillAmount > 0)
            transform.LookAt(Player.position);
    }

    void Attacked()
    {
        if (transform.GetComponent<EnemyHP>().IsDamaged)
        {
            Invoke("attackable", 0.35f);
            ClipStateControl(false, false, false, false, true);
            AIagent.enabled = false;
            AIagent.speed = 0;
            transform.GetComponent<EnemyHP>().IsDamaged = false;
        }
    }

    void attackable()
    {
        if (Distance <= attackrange)
            ComboAttack = true;
    }

    void AttackAni()
    {
        if (ComboAttack)
        {
            ClipStateControl(true, false, false, false, false);
            ComboAttack = false;
        }
        if (MummyAni.GetCurrentAnimatorStateInfo(0).IsName("Attack1")) attackcenter(false, true, false);
        if (MummyAni.GetCurrentAnimatorStateInfo(0).IsName("Attack2")) attackcenter(false, false, true);
        if (MummyAni.GetCurrentAnimatorStateInfo(0).IsName("Attack3")) attackcenter(true, false, false);
    }

    void attackcenter(bool attack1, bool attack2, bool attack3)
    {
        if (MummyAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            MummyAni.SetBool("Attack", attack1);
            MummyAni.SetBool("Attack1", attack2);
            MummyAni.SetBool("Attack2", attack3);
        }
    }
    
    void ClipStateControl(bool Attack, bool Attack1, bool Attack2, bool Move, bool Attacked)
    {
        MummyAni.SetBool("Attack", Attack);
        MummyAni.SetBool("Attack1", Attack1);
        MummyAni.SetBool("Attack2", Attack2); 
        MummyAni.SetBool("Move", Move);
        MummyAni.SetBool("Attacked", Attacked);
    }

    void DelaySpeed()
    {
        AIagent.speed = speed;
    }

    void DeadAni()
    {
        if (GetComponent<EnemyHP>().HpImg.fillAmount <= 0 && !IsDead)
        {
            ClipStateControl(false, false, false, false, false);
            ce.monsterNum -= 1;
            ce.EnemyList.Remove(this.gameObject);
            MummyAni.SetBool("Dead", true);
            AIagent.speed = 0;
            AIagent.enabled = false;
            EC.IsExp = true;
            EC.currentval = exp * (1 + 0.01f * status.Calgetexp);
            IsDead = true;
            Invoke("DestroyObj", 2f);
        }

    }

    void DestroyObj()
    {
        Destroy(transform.parent.gameObject);
    }
}
