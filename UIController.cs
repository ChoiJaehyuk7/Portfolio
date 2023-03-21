using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject mapBtn;
    public GameObject statusBtn, skillBtn, questBtn, invenBtn;
    public GameObject soundBtn, exitBtn;
    public GameObject activeStatus, activeSkill, actives;
    bool ismap, isinfo, isoption;

    [HideInInspector] public MapController mc;
    [HideInInspector] public Inventory inven;
    [HideInInspector] public SkillManager sm;
    [HideInInspector] public AbilityController ac;
    [HideInInspector] public QuestUI qu;
    public SoundClip sound;
    // Start is called before the first frame update
    void Start()
    {
        SetActivate(false, false, false, false, false, false, false);
        activeStatus.SetActive(false);
        activeSkill.SetActive(false);
        actives.SetActive(false);
    }

    void clickSound()
    {
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.I))
            sound.sounds[1].Play();
    }

    public void clickSoundButton()
    {
        sound.sounds[1].Play();
    }

    void Update()
    {
        if(ismap)
            SetActivate(true, false, false, false, false, false, false);
        else if(isinfo)
            SetActivate(false, true, true, true, true, false, false);
        else if(isoption)
            SetActivate(false, false, false, false, false, true, true);
        else
            SetActivate(false, false, false, false, false, false, false);

        activation();
        clickSound();
    }

    void activation()
    {
        if(sm.skillpoint > 0 && ac.remainvalue > 0)
        {
            actives.SetActive(true);
            activeSkill.SetActive(true);
            activeStatus.SetActive(true);
        }
        else if(sm.skillpoint > 0)
        {
            actives.SetActive(true);
            activeSkill.SetActive(true);
            activeStatus.SetActive(false);
        }
        else if(ac.remainvalue > 0)
        {
            actives.SetActive(true);
            activeStatus.SetActive(true);
            activeSkill.SetActive(false);
        }
        else
        {
            actives.SetActive(false);
            activeSkill.SetActive(false);
            activeStatus.SetActive(false);
        }
    }
    void SetActivate(bool map, bool status, bool skill, bool quest, bool inven, bool sound, bool exit)
    {
        mapBtn.SetActive(map);
        statusBtn.SetActive(status);
        skillBtn.SetActive(skill);
        questBtn.SetActive(quest);
        invenBtn.SetActive(inven);
        soundBtn.SetActive(sound);
        exitBtn.SetActive(exit);
    }
    public void mapButton()
    {
        ismap = !ismap;
        isinfo = false;
        isoption = false;
    }

    public void infoButton()
    {
        isinfo = !isinfo;
        ismap = false;
        isoption = false;
    }

    public void optionButton()
    {
        isoption = !isoption;
        ismap = false;
        isinfo = false;
    }

    public void clickMap()
    {
        mc.Map.SetActive(true);
        mc.activeMap = true;
        mc.Map.transform.SetAsLastSibling();
        ismap = false;
    }

    public void clickStatus()
    {
        ac.StatusPanel.SetActive(true);
        ac.activeStatus = true;
        ac.StatusPanel.transform.SetAsLastSibling();
        isinfo = false;
    }

    public void clickSkill()
    {
        sm.Panel.SetActive(true);
        sm.actSkill = true;
        sm.contentspanel.transform.position = new Vector2(sm.contentspanel.transform.position.x, -650);
        sm.Panel.transform.SetAsLastSibling();
        isinfo = false;
    }

    public void clickQuest()
    {
        qu.panelUI.SetActive(true);
        qu.isActive = true;
        qu.panelUI.transform.SetAsLastSibling();
        isinfo = false;
    }

    public void clickInven()
    {
        inven.InvenPanel.SetActive(true);
        inven.activeInven = true;
        inven.InvenPanel.transform.SetAsLastSibling();
        isinfo = false;
    }
}
