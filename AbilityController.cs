using UnityEngine;
using TMPro;

public class AbilityController : MonoBehaviour
{
    //activate parameter
    public bool activeStatus;
    public GameObject StatusPanel;
    public GameObject ButtonHp, ButtonMp, ButtonDex, ButtonLuck;

    public TextMeshProUGUI RemainValueText;
    public GameObject RemainText;

    [HideInInspector] public GameObject infoDexObj,infoHpObj,infoLuckObj,infoMpObj;
    [HideInInspector] public SoundClip sound;
    //status parameter
    public float remainvalue; //·¹º§¾÷ ÈÄ È¹µæÇÏ´Â ½ºÅÈÄ¡
    [HideInInspector] public Status status;
    InformationDex infodex;
    InformationHP infohp;
    InformationLuck infoluck;
    InformationMP infomp;
    void Start()
    {
        StatusPanel.SetActive(false);
        ButtonDex.SetActive(false);
        ButtonLuck.SetActive(false);
        ButtonHp.SetActive(false);
        ButtonMp.SetActive(false);

        infodex = infoDexObj.GetComponent<InformationDex>();
        infohp = infoHpObj.GetComponent<InformationHP>();
        infoluck = infoLuckObj.GetComponent<InformationLuck>();
        infomp = infoMpObj.GetComponent<InformationMP>();
    } 

    // Update is called once per frame
    void Update()
    {
        Activation();
    }

    void Activation()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            activeStatus = !activeStatus;
            if (!activeStatus)
            {
                infodex.PanelDexInfo.SetActive(false);
                infohp.PanelHpInfo.SetActive(false);
                infoluck.PanelLuckInfo.SetActive(false);
                infomp.PanelMpInfo.SetActive(false);
            }

            StatusPanel.SetActive(activeStatus); 
            StatusPanel.transform.SetAsLastSibling();
        }

        if (activeStatus)
        {
            if (remainvalue >= 1)
            {
                ButtonDex.SetActive(true);
                ButtonLuck.SetActive(true);
                ButtonHp.SetActive(true);
                ButtonMp.SetActive(true);

                RemainText.SetActive(true);
                RemainValueText.enabled = true;
                RemainValueText.text = remainvalue.ToString();

            }
            else
            {
                ButtonDex.SetActive(false);
                ButtonLuck.SetActive(false);
                ButtonHp.SetActive(false);
                ButtonMp.SetActive(false);

                RemainText.SetActive(false);
                RemainValueText.enabled = false;
            }
        }
    }
    public void ExitUI()
    {
        StatusPanel.SetActive(false);
        activeStatus = false;
    }

    public void ClickMpButton()
    {
        status.IsMp = true;
        sound.sounds[2].Play();
        remainvalue -= 1;   
    }

    public void ClickHpButton()
    {
        status.IsHp = true;
        sound.sounds[2].Play();
        remainvalue -= 1;  
    }
    public void ClickDexButton()
    {
        status.IsDex = true;
        sound.sounds[2].Play();
        remainvalue -= 1;
    }
    public void ClickLuckButton()
    {
        status.IsLuck = true;
        sound.sounds[2].Play();
        remainvalue -= 1;
    }
}
