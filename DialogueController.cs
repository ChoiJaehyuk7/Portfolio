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
                //Ŭ����� -> ��ȭ
                guidePanel.SetActive(false);
                break;
            case 2:
                break;
            case 3:
                pm.GetComponent<NavMeshAgent>().enabled = false;    //ĳ���� ��ġ ���� ������ ���·� �����
                pm.transform.position = new Vector3(-117, 1, -112); //ĳ���� ���� ������
                pm.GetComponent<NavMeshAgent>().enabled = true;     //ĳ���� �̵� ������ ���·� �����
                qb.initbuttons[0].gameObject.SetActive(false); // ���۰��� ����Ʈ��ư ��Ȱ��ȭ
                qb.ingbuttons[0].gameObject.SetActive(true);   // ������ ����Ʈ��ư Ȱ��ȭ
                qb.ingbuttons[0].ing.SetActive(true);          // ����Ʈ[0] ���� �� Ȱ��ȭ
                qb.ingbuttons[0].done.SetActive(false);        // ����Ʈ[0] ���۰��� ��Ȱ��ȭ
                qb.ingbuttons[0].goalValue = null;             // ����Ʈ[0] ���� �� ��ǥ�� ���� x
                npcs[0].prefOn.SetActive(false);               // npc[0] �Ӹ� �� ����ǥ ��Ȱ��ȭ
                miniDone.transform.position = new Vector3(npcs[1].transform.position.x, 105, npcs[1].transform.position.z); //npc[1] �̴ϸ� �� �Ϸ�ǥ�� ������ ����
                miniDone.SetActive(true);                      //�̴ϸ� ����ǥ Ȱ��ȭ
                miniOn.SetActive(false);                       //�̴ϸ� ����ǥ ��Ȱ��ȭ
                MessageUI.SetActive(false);                    //�޽��� UI ��Ȱ��ȭ
                npcs[0].isMessage = false;                     //npc[0]�� ��ȣ�ۿ� ���� ����
                pt.istriggerText = true;
                create.temp = create.currRegion[0];
                sequence += 1;                                 
                break;
            case 4:
                qb.ingbuttons[0].ing.SetActive(false);         //����Ʈ[0] ���� �� ������ ǥ��
                qb.ingbuttons[0].done.SetActive(true);         //ing -> done ���� ����
                npcs[1].prefDone.SetActive(true);              //npc[1] �Ӹ� �� ����ǥ Ȱ��ȭ
                npcstring = npcs[1].npcname;                   //�޽��� NPC �� npc[1].name���� ����
                break;
            case 5:
                synario[sequence - 2] = false;                 //synario 3�� �Ҽ�ȭ�Ǿ��ִ� ���� �ذ��
                guidePanel.SetActive(false);                   //��ȣ�ۿ� panel ��Ȱ��ȭ
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
                npcs[1].prefDone.SetActive(false);             //npc[1] �Ӹ� �� ����ǥ ��Ȱ��ȭ
                npcs[1].prefOn.SetActive(true);                //npc[1] �Ӹ� �� ����ǥ Ȱ��ȭ
                miniDone.SetActive(false);                     //�̴ϸ� �� �Ϸ�ǥ�� ��Ȱ��ȭ
                miniOn.SetActive(true);                        //�̴ϸ� �� ����ǥ Ȱ��ȭ
                miniOn.transform.position = new Vector3(npcs[1].transform.position.x, 105, npcs[1].transform.position.z);  //�̴ϸ� �� ����ǥ ������ ����
                qb.donebuttons[0].gameObject.SetActive(true);  //����Ʈ[0] �Ϸ� â Ȱ��ȭ
                qb.ingbuttons[0].gameObject.SetActive(false);  //����Ʈ[0] ���� �� ��Ȱ��ȭ
                qb.initbuttons[1].gameObject.SetActive(true);  //����Ʈ[1] ���� ���� Ȱ��ȭ
                MessageUI.SetActive(false);                    //�޽��� UI ��Ȱ��ȭ
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
                npcs[1].prefOn.SetActive(false);               //npc[1] �Ӹ� �� ����ǥ ��Ȱ��ȭ
                qb.initbuttons[1].gameObject.SetActive(false); //����Ʈ[1] ���� ���� ��Ȱ��ȭ
                qb.ingbuttons[1].gameObject.SetActive(true);   //����Ʈ[1] ���� �� Ȱ��ȭ
                miniOn.SetActive(false);                       //�̴ϸ� �� ����ǥ ��Ȱ��ȭ
                range[0].SetActive(true);                      //�̴ϸ� �� ����Ʈ ���� ���� Ȱ��ȭ
                MessageUI.SetActive(false);                    //�޽��� UI ��Ȱ��ȭ
                guideText.text = "ä���ϱ�";                   //��ȣ�ۿ� Text string ����
                qb.ingbuttons[1].goalValueText.text = pt.MushroomValue.ToString() + " / 5"; //����Ʈ[1] ���� �� ȹ�� ���� Text ǥ��
                break;
            case 10:
                range[0].SetActive(false);
                npcs[1].prefOn.transform.localPosition = new Vector3(0, 0, 0);
                npcs[1].prefOn.transform.localRotation = Quaternion.Euler(new Vector3(35f, 0, 0));
                npcs[1].prefOn.transform.localScale = new Vector3(3, 3, 2);
                qb.ingbuttons[1].ing.SetActive(false);         //����Ʈ[1] ���� �� ������ ǥ��
                qb.ingbuttons[1].done.SetActive(true);         //ing -> done ���� ����
                npcs[1].prefDone.SetActive(true);              //npc[1] �Ӹ� �� ����ǥ Ȱ��ȭ
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
                qb.ingbuttons[1].gameObject.SetActive(false);  //����Ʈ[1] ���� �� ��Ȱ��ȭ
                qb.donebuttons[1].gameObject.SetActive(true);  //����Ʈ[1] �Ϸ� â Ȱ��ȭ
                qb.initbuttons[2].gameObject.SetActive(true);  //����Ʈ[2] ���� ���� Ȱ��ȭ
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
