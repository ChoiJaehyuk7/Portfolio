using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class EnemyAI : MonoBehaviour
{
    NavMeshAgent AIagent;
    Transform Player;

    public Animator ani;
    float AniCreateTime, direcTimer;
    public float Distance;
    bool IsCreate, IsDead;

    public float speed, attackrange, chaserange;
    public int exp;
    Vector3 aiDirection;
    bool isPatrol;

    ExpCointroller EC;
    CreateEnemy ce;
    Status status;
    PlayerHP ph;
    public new Collider collider;
    public Collider collider2;
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
        //Distance = Vector3.Distance(this.transform.position, Player.transform.position);
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
                Patrolling();
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
                ClipStateControl(false, true, false);
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
        ClipStateControl(false, true, false);
        AIagent.speed = speed;

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
        {
            direcTimer += Time.deltaTime;
        }

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
            direcTimer = 0;
            ClipStateControl(false, true, false);
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
        direcTimer = 0;
        ClipStateControl(true, false, false);
        AIagent.speed = 0;
        if (GetComponent<EnemyHP>().HpImg.fillAmount > 0)
            transform.LookAt(Player.position);
    }

    void Attacked()
    {
        if (transform.GetComponent<EnemyHP>().IsDamaged)
        {
            ClipStateControl(false, false, true);
            //AIagent.enabled = false;
            AIagent.speed = 0;
            transform.GetComponent<EnemyHP>().IsDamaged = false;
        }
    }
    void ClipStateControl(bool Attack, bool Move, bool Attacked)
    {
        ani.SetBool("Attack", Attack);
        ani.SetBool("Move", Move);
        ani.SetBool("Attacked", Attacked);
    }

    void DelaySpeed()
    {
        AIagent.speed = speed;
    }

    void DeadAni()
    {
        if (GetComponent<EnemyHP>().HpImg.fillAmount <= 0 && !IsDead)
        {
            ClipStateControl(false, false, false);
            ani.SetBool("Dead", true);
            ce.monsterNum -= 1;
            ce.EnemyList.Remove(this.gameObject);
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
        if (this.gameObject.CompareTag("Ghoul") || this.gameObject.CompareTag("Golem"))
            Destroy(transform.parent.gameObject.transform.parent.gameObject);
        else
            Destroy(transform.parent.gameObject);
    }
}
