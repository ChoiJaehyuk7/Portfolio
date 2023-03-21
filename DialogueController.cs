using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class DialogueController : MonoBehaviour
{
    public bool first;
    public bool[] synario;
    public NPC[] npcs;
    [TextArea] public string[] dialogue;
    string npcstring;
    public TextMeshProUGUI message, npcname;
    public GameObject MessageUI;
    public GameObject miniOn;
    public GameObject miniDone;
    public GameObject[] range;
    public GameObject guidePanel;
    public int rewardA, rewardB;
    public TextMeshProUGUI rewardAtxt, rewardBtxt;
    public TextMeshProUGUI guideText;
    [HideInInspector] public QuestButtonController qb;
    [HideInInspector] public PlayerMove pm;
    [HideInInspector] public PlayerTrigger pt;
    [HideInInspector] public CreateEnemy create;
    [HideInInspector] public ExpCointroller ec;
    [HideInInspector] public SkillManager sm;
    public int sequence;
    bool a;
    // Update is called once per frame
    private void Start()
    {
        first = true;
        MessageUI.SetActive(false);
        miniOn.SetActive(false);
        miniDone.SetActive(false);
        guidePanel.SetActive(false);
        for(int i = 0; i< range.Length; i++)
            range[i].SetActive(false);
    }
    void Update()
    {
        NPCcontroller();
        if (synario[sequence])
        {
            message.text = dialogue[sequence];
            npcname.text = npcstring;
        }
        SynarioProgress();
    }

    void NPCcontroller()
    {
        if(sequence == 10)
        {
            npcs[1].a = 1;
        }
        else if (sequence == 9)
        {
            npcs[1].a = 99;
        }
        else if(3 <= sequence && sequence <= 5)
        {
            npcs[1].a = 1;
        }
        else if(sequence <= 2)
        {
            npcs[0].a = 0;
        }
    }

    void SynarioProgress()
    {
        switch (sequence)
        {
            case 0:
                npcs[0].prefOn.SetActive(true);
                if (synario[0])
                    npcs[0].prefOn.transform.localRotation = Quaternion.Euler(new Vector3(21.6f, 39.5f, 0));
                npcstring = npcs[0].npcname;
                miniOn.transform.position = npcs[0].transform.position;
                miniOn.SetActive(true);
                miniOn.transform.position = new Vector3(npcs[0].transform.position.x, 105, npcs[0].transform.position.z);
                qb.initbuttons[0].gameObject.SetActive(true);
                npcstring = npcs[0].npcname;
                break;
            case 1:
                //클로즈업 -> 대화
                guidePanel.SetActive(false);
                break;
            case 2:
                break;
            case 3:
                pm.GetComponent<NavMeshAgent>().enabled = false;    //캐릭터 위치 변경 가능한 상태로 만들기
                pm.transform.position = new Vector3(-117, 1, -112); //캐릭터 변경 포지션
                pm.GetComponent<NavMeshAgent>().enabled = true;     //캐릭터 이동 가능한 상태로 만들기
                qb.initbuttons[0].gameObject.SetActive(false); // 시작가능 퀘스트버튼 비활성화
                qb.ingbuttons[0].gameObject.SetActive(true);   // 진행중 퀘스트버튼 활성화
                qb.ingbuttons[0].ing.SetActive(true);          // 퀘스트[0] 진행 중 활성화
                qb.ingbuttons[0].done.SetActive(false);        // 퀘스트[0] 시작가능 비활성화
                qb.ingbuttons[0].goalValue = null;             // 퀘스트[0] 진행 중 목표값 설정 x
                npcs[0].prefOn.SetActive(false);               // npc[0] 머리 위 느낌표 비활성화
                miniDone.transform.position = new Vector3(npcs[1].transform.position.x, 105, npcs[1].transform.position.z); //npc[1] 미니맵 상 완료표시 포지션 설정
                miniDone.SetActive(true);                      //미니맵 물음표 활성화
                miniOn.SetActive(false);                       //미니맵 느낌표 비활성화
                MessageUI.SetActive(false);                    //메시지 UI 비활성화
                npcs[0].isMessage = false;                     //npc[0]과 상호작용 가능 해제
                pt.istriggerText = true;
                create.temp = create.currRegion[0];
                sequence += 1;                                 
                break;
            case 4:
                qb.ingbuttons[0].ing.SetActive(false);         //퀘스트[0] 진행 중 아이콘 표시
                qb.ingbuttons[0].done.SetActive(true);         //ing -> done 으로 변경
                npcs[1].prefDone.SetActive(true);              //npc[1] 머리 위 물음표 활성화
                npcstring = npcs[1].npcname;                   //메시지 NPC 명 npc[1].name으로 변경
                break;
            case 5:
                synario[sequence - 2] = false;                 //synario 3번 할성화되어있는 문제 해결안
                guidePanel.SetActive(false);                   //상호작용 panel 비활성화
                npcs[1].prefDone.transform.localPosition = new Vector3(-0.2f, -0.5f, -0.04f);
                npcs[1].prefDone.transform.localRotation = Quaternion.Euler(new Vector3(35f, 48.18f, 5.3f));
                npcs[1].prefDone.transform.localScale = new Vector3(1, 1, 1);
                a = true;
                break;
            case 6:
                if (a)
                {
                    ec.IsExp = true;
                    ec.currentval = 90;
                    sm.skillpoint += 60;
                    a = false;
                }
                npcs[1].prefDone.SetActive(false);             //npc[1] 머리 위 물음표 비활성화
                npcs[1].prefOn.SetActive(true);                //npc[1] 머리 위 느낌표 활성화
                miniDone.SetActive(false);                     //미니맵 상 완료표시 비활성화
                miniOn.SetActive(true);                        //미니맵 상 느낌표 활성화
                miniOn.transform.position = new Vector3(npcs[1].transform.position.x, 105, npcs[1].transform.position.z);  //미니맵 상 느낌표 포지션 설정
                qb.donebuttons[0].gameObject.SetActive(true);  //퀘스트[0] 완료 창 활성화
                qb.ingbuttons[0].gameObject.SetActive(false);  //퀘스트[0] 진행 중 비활성화
                qb.initbuttons[1].gameObject.SetActive(true);  //퀘스트[1] 시작 가능 활성화
                MessageUI.SetActive(false);                    //메시지 UI 비활성화
                break;
            case 7:
                npcs[1].prefOn.transform.localPosition = new Vector3(-0.16f, -0.33f, 0);
                npcs[1].prefOn.transform.localRotation = Quaternion.Euler(new Vector3(35f, 42.9f, 0));
                npcs[1].prefOn.transform.localScale = new Vector3(1, 1, 1);
                rewardA = 120;
                rewardB = 100;
                rewardAtxt.text = rewardA.ToString();
                rewardBtxt.text = rewardB.ToString();
                guidePanel.SetActive(false);
                break;
            case 8:
                npcs[1].prefDone.transform.localPosition = new Vector3(0f, 0f, 0f);
                npcs[1].prefDone.transform.localRotation = Quaternion.Euler(new Vector3(35f, 0, 0));
                npcs[1].prefDone.transform.localScale = new Vector3(3, 3, 2);
                break;
            case 9:
                pt.isQuest1 = true;
                npcs[1].prefOn.SetActive(false);               //npc[1] 머리 위 느낌표 비활성화
                qb.initbuttons[1].gameObject.SetActive(false); //퀘스트[1] 시작 가능 비활성화
                qb.ingbuttons[1].gameObject.SetActive(true);   //퀘스트[1] 진행 중 활성화
                miniOn.SetActive(false);                       //미니맵 상 느낌표 비활성화
                range[0].SetActive(true);                      //미니맵 상 퀘스트 진행 범위 활성화
                MessageUI.SetActive(false);                    //메시지 UI 비활성화
                guideText.text = "채집하기";                   //상호작용 Text string 변경
                qb.ingbuttons[1].goalValueText.text = pt.MushroomValue.ToString() + " / 5"; //퀘스트[1] 진행 중 획득 버섯 Text 표시
                break;
            case 10:
                range[0].SetActive(false);
                npcs[1].prefOn.transform.localPosition = new Vector3(0, 0, 0);
                npcs[1].prefOn.transform.localRotation = Quaternion.Euler(new Vector3(35f, 0, 0));
                npcs[1].prefOn.transform.localScale = new Vector3(3, 3, 2);
                qb.ingbuttons[1].ing.SetActive(false);         //퀘스트[1] 진행 중 아이콘 표시
                qb.ingbuttons[1].done.SetActive(true);         //ing -> done 으로 변경
                npcs[1].prefDone.SetActive(true);              //npc[1] 머리 위 물음표 활성화
                miniDone.SetActive(true);
                break;
            case 11:
                Debug.Log("11");
                break;
            case 12:
                if (!a)
                {
                    ec.IsExp = true;
                    ec.currentval = rewardA;
                    sm.skillpoint += rewardB;
                    a = true;
                }
                miniDone.SetActive(false);
                npcs[1].prefOn.SetActive(true);
                npcs[1].prefDone.SetActive(false);
                miniOn.SetActive(true);
                qb.ingbuttons[1].gameObject.SetActive(false);  //퀘스트[1] 진행 중 비활성화
                qb.donebuttons[1].gameObject.SetActive(true);  //퀘스트[1] 완료 창 활성화
                qb.initbuttons[2].gameObject.SetActive(true);  //퀘스트[2] 시작 가능 활성화
                MessageUI.SetActive(false);
                Debug.Log("12");
                break;
            case 13:
                Debug.Log("13");
                break;
            case 14:
                Debug.Log("14");
                break;
            case 15:
                Debug.Log("15");
                break;
            case 16:
                Debug.Log("16");
                break;
        }
    }
}
