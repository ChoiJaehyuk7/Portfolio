using UnityEngine;

public class NPC : MonoBehaviour
{
    public int npcID;
    public int a;
    public string npcname;
    [HideInInspector] public DialogueController dialogue;
    public SoundClip sound;
    public GameObject prefOn;
    public GameObject prefDone;

    public bool istrigger;
    public bool isMessage;
    private void Start()
    {
        prefOn.SetActive(false);
        prefDone.SetActive(false);
    }
    private void Update()
    {
        MessageControl();
    }

    void MessageControl()
    {
        if (istrigger)
        {
            dialogue.guideText.text = "대화하기";
            if (Input.GetKeyDown(KeyCode.V))
            {
                dialogue.guidePanel.SetActive(false);
                dialogue.MessageUI.SetActive(true);
                isMessage = true;

                if (dialogue.sequence == 0)
                    dialogue.synario[dialogue.sequence] = true;
                else
                {
                    dialogue.synario[dialogue.sequence + 1] = true;
                    dialogue.synario[dialogue.sequence] = false;
                    dialogue.sequence++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                sound.sounds[7].Play();
                if (dialogue.sequence != 4 || dialogue.sequence != 6 || dialogue.sequence != 10 || dialogue.sequence != 12)
                    if (isMessage)
                    {
                        dialogue.synario[dialogue.sequence + 1] = true;
                        dialogue.synario[dialogue.sequence] = false;
                        dialogue.sequence++;
                    }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (a == npcID)
        {
            if (other.gameObject.CompareTag("Player"))
                istrigger = true;
        }
        else
            istrigger = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (a == npcID)
            istrigger = false;
    }
}
