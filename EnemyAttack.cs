using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHP ph;
    PlayerTrigger pt;
    CommandCenter cc;
    Status status;
    RandomSeed rs;
    int AttackDmg;
    public int Dmg;
    public int AttackID;
    private void Start()
    {
        status = GameObject.FindWithTag("StatusController").GetComponent<Status>();
        pt = GameObject.FindWithTag("Player").GetComponent<PlayerTrigger>();
        ph = GameObject.FindWithTag("Player").GetComponent<PlayerHP>();
        cc = GameObject.FindWithTag("CommandCenter").GetComponent<CommandCenter>();
        rs = GameObject.FindWithTag("RandomSeed").GetComponent<RandomSeed>();
    }
    void asdf()
    {
        switch (AttackID)
        {
            case 1:
                AttackDmg = (int)(Dmg + Random.Range(-3, 3) - status.Calarmor);
                break;
            case 2:
                AttackDmg = (int)(Dmg + Random.Range(-4, 4) - status.Calarmor);
                break;
            case 3:
                AttackDmg = (int)(Dmg + Random.Range(-5, 5) - status.Calarmor);
                break;
            case 4:
                AttackDmg = (int)(Dmg + Random.Range(-6, 6) - status.Calarmor);
                break;
            case 5:
                AttackDmg = (int)(Dmg + Random.Range(-7, 7) - status.Calarmor);
                break;
            case 6:
                AttackDmg = (int)(Dmg + Random.Range(-8, 8) - status.Calarmor);
                break;
            case 7:
                AttackDmg = (int)(Dmg + Random.Range(-10, 10) - status.Calarmor);
                break;
        }

        cc.SaveVec = ph.transform.position;
        if (!pt.isBuff)
        {
            if (status.Calevasion > rs.percent)
            {
                cc.DamagedMissText("회피");
            }
            else
            {
                if (AttackDmg > 0)
                {
                    ph.HP -= AttackDmg;
                    cc.DamagedText(AttackDmg);
                }
                else
                {
                    cc.DamagedText(0);
                }
            }
           
        }
        else
        {
            cc.DamagedMissText("면역");
        }

    }
}
