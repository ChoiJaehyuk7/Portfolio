using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class CreateEnemy : MonoBehaviour
{
    public GameObject preMummy;
    public GameObject prebigMummy;
    public GameObject preGhoul;
    public GameObject preDemon;
    public GameObject preCrab;
    public GameObject preGolem;
    public GameObject preWhale;

    //public GameObject[] region;
    public GameObject[] spawn;
    public BoxCollider[] spawnCollier;
    [HideInInspector] public string[] currRegion;
    [HideInInspector] public PlayerTrigger trigger;

    public List<GameObject> EnemyList = new List<GameObject>();

    public GameObject player;
    public string temp;
    public float spawnTime;
    public int monsterNum;
    public TextMeshProUGUI regionText;
    public GameObject regionUI;
    public int nums;
     float Triggertimer, Spawntimer, Createtimer;
    private void Start()
    {
        regionUI.SetActive(false);

        for(int i=0; i<spawn.Length; i++)
            spawnCollier[i] = spawn[i].GetComponent<BoxCollider>();

        player = GameObject.Find("Character").gameObject;

        StartCoroutine(enemyDist());
    }
    void Update()
    {
        TriggerRegion();
        MonsterSpawn();
    }

    void TriggerRegion()
    {
        if (trigger.istriggerText)
        {
            if (Triggertimer >= 1)
            {
                trigger.istriggerText = false;
                regionUI.SetActive(false);
                Triggertimer = 0;
            }
            else
            {
                regionUI.SetActive(true);
                Triggertimer += Time.deltaTime;
                regionText.text = temp;
            }
        }

        if (trigger.one) temp = (temp == currRegion[0]) ? currRegion[1] : currRegion[0];
        else if (trigger.two) temp = (temp == currRegion[1]) ? currRegion[2] : currRegion[1];
        else if (trigger.thr) temp = (temp == currRegion[2]) ? currRegion[3] : currRegion[2];
        else if (trigger.four) temp = (temp == currRegion[2]) ? currRegion[4] : currRegion[2];
        else if (trigger.five) temp = (temp == currRegion[2]) ? currRegion[5] : currRegion[2];
        else if (trigger.six) temp = (temp == currRegion[5]) ? currRegion[6] : currRegion[5];
        else if (trigger.sev) temp = (temp == currRegion[1]) ? currRegion[3] : currRegion[1];

        if (trigger.one || trigger.two || trigger.thr || trigger.four || trigger.five || trigger.six || trigger.sev)
        {
            trigger.one = false;
            trigger.two = false;
            trigger.thr = false;
            trigger.four = false;
            trigger.five = false;
            trigger.six = false;
            trigger.sev = false;
        }
    }

    IEnumerator enemyDist()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            for (int i = 0; i < EnemyList.Count; i++)
            {
                float distance = Vector3.Distance(player.transform.position, EnemyList[i].transform.position);

                if (distance >= 90)
                {
                    Destroy(EnemyList[i]);
                    EnemyList.Remove(EnemyList[i]);
                    monsterNum -= 1;
                }
            }
        }
    }

    void MonsterSpawn()
    {
        if (monsterNum < nums)
        {
            Vector3 spawnPos;
            switch (temp)
            {
                case "카일룸 성채 입구":
                    spawnPos = CreateController(1);
                    InstantiateMonster(spawnPos, 1);
                    break;
                case "풍구스 거리":
                    spawnPos = CreateController(2);
                    InstantiateMonster(spawnPos, 2);
                    break;
                case "파로스 골목":
                    spawnPos = CreateController(3);
                    InstantiateMonster(spawnPos, 3);
                    break;
                case "에클레시아 성당":
                    spawnPos = CreateController(4);
                    InstantiateMonster(spawnPos, 4);
                    break;
                case "에우로스 성벽":
                    spawnPos = CreateController(5);
                    InstantiateMonster(spawnPos, 5);
                    break;
                case "리투스 거리":
                    spawnPos = CreateController(6);
                    InstantiateMonster(spawnPos, 6);
                    break;
            }
        }
    }
    void ra(Vector3 pos, int rv)
    {
        int rand = Random.Range(0, 10);

        int val = rand % 10;

        if (rv == 1)
        {//mummy : bigmummy  = 8 : 2
            if (val < 8)
                Instantiate(preMummy, pos, Quaternion.identity);
            else
                Instantiate(prebigMummy, pos, Quaternion.identity);

        }
        else if (rv == 2)
        {//mummy : bigmummy : ghoul = 6 : 3 : 1
            if(val < 6)
                Instantiate(preMummy, pos, Quaternion.identity);
            else if (val < 9)
                Instantiate(prebigMummy, pos, Quaternion.identity);
            else
                Instantiate(preGhoul, pos, Quaternion.identity);
        }
        else if (rv == 3)
        { //bigmummy : ghoul = 7 : 3
            if (val < 7)
                Instantiate(prebigMummy, pos, Quaternion.identity);
            else
                Instantiate(preGhoul, pos, Quaternion.identity);
        }
        else if (rv == 4)
        {//demon : golem : ghoul = 3 : 3 : 4
            if (val < 3)
                Instantiate(preDemon, pos, Quaternion.identity);
            else if (val < 6)
                Instantiate(preGolem, pos, Quaternion.identity);
            else
                Instantiate(preGhoul, pos, Quaternion.identity);
        }
        else if (rv == 5)
        { //whale : golem : ghoul : demon = 2 : 2 : 4 : 2 
            if (val < 2)
               Instantiate(preWhale, pos, Quaternion.identity);
            else if (val < 4)
                Instantiate(preGolem, pos, Quaternion.identity);
            else if (val < 8)
                Instantiate(preGhoul, pos, Quaternion.identity);
            else
                Instantiate(preDemon, pos, Quaternion.identity);
        }
        else if (rv == 6)
        {//golem : crab : whale = 4 : 3 : 3
            if (val < 4)
                Instantiate(preGolem, pos, Quaternion.identity);
            else if (val < 7)
                Instantiate(preCrab, pos, Quaternion.identity);
            else
                Instantiate(preWhale, pos, Quaternion.identity);
        }
    }

    void InstantiateMonster(Vector3 pos, int regionValue)
    {
        if (monsterNum < nums)
        {
            if (Spawntimer > spawnTime)
            {
                ra(pos, regionValue);
                monsterNum += 1;
                Spawntimer = 0;
            }
            else
            {
                Spawntimer += Time.deltaTime;
            }
        }
    }

    Vector3 CreateController(int indexRegion)
    {
        Vector3 spawnPos = Vector3.zero;
        if (temp == currRegion[indexRegion])
        {
            int randCreate = RandomExtract(indexRegion);
            if (Createtimer > spawnTime)
            {
                float posX = spawn[randCreate].transform.position.x + Random.Range(-spawnCollier[randCreate].size.x * 0.5f, spawnCollier[randCreate].size.x * 0.5f);
                float posZ = spawn[randCreate].transform.position.z + Random.Range(-spawnCollier[randCreate].size.z * 0.5f, spawnCollier[randCreate].size.z * 0.5f);
                spawnPos = new Vector3(posX, 3, posZ);
                Createtimer = 0;
            }
            else
                Createtimer += Time.deltaTime;
        }
        return spawnPos;
    }

    int RandomExtract(int index)
    {
        int[] value = new int[4];
        int rand = Random.Range(0, 4);

        switch (index)
        {
            case 1:
                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                break;
            case 2:
                value[0] = 3;
                value[1] = 4;
                value[2] = 5;
                value[3] = 6;
                break;
            case 3:
                value[0] = 6;
                value[1] = 7;
                value[2] = 11;
                value[3] = 12;
                break;
            case 4:
                value[0] = 6;
                value[1] = 8;
                value[2] = 9;
                value[3] = 10;
                break;
            case 5:
                value[0] = 4;
                value[1] = 5;
                value[2] = 13;
                value[3] = 14;
                break;
            case 6:
                value[0] = 13;
                value[1] = 14;
                value[2] = 15;
                value[3] = 16;
                break;
        }
        return value[rand % 4];
    }
}
