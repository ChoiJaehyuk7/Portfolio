using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public GameObject panelUI;
    public GameObject contentUI;
    public GameObject panelinit, panelprogress, paneldone;
    public GameObject initButton, ingButton, doneButton;
    public bool isActive;
    public bool Click;
    void Start()
    {
        panelUI.SetActive(false);
        contentUI.SetActive(false);
        panelControl(true, false, false);

        initButton.GetComponent<Image>().color = new Color(0.58f, 0.58f, 0.4f);
        ingButton.GetComponent<Image>().color = new Color(0.82f, 0.82f, 0.43f);
        doneButton.GetComponent<Image>().color = new Color(0.82f, 0.82f, 0.43f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {

            isActive = !isActive;
            panelUI.SetActive(isActive);
            panelUI.transform.SetAsLastSibling();
            panelControl(true, false, false);

            initButton.GetComponent<Image>().color = new Color(0.58f, 0.58f, 0.4f);
            ingButton.GetComponent<Image>().color = new Color(0.82f, 0.82f, 0.43f);
            doneButton.GetComponent<Image>().color = new Color(0.82f, 0.82f, 0.43f);
        }
    }
    void panelControl(bool init, bool ing, bool done)
    {
        panelinit.SetActive(init);
        panelprogress.SetActive(ing);
        paneldone.SetActive(done);
    }
    public void exitpanel()
    {
        isActive = false;
        panelUI.SetActive(false);
    }
    public void exitcontent()
    {
        contentUI.SetActive(false);
    }
    public void clickInit()
    {
        panelControl(true, false, false);
        Click = true;
        initButton.GetComponent<Image>().color = new Color(0.58f, 0.58f, 0.4f);
        ingButton.GetComponent<Image>().color = new Color(0.82f, 0.82f, 0.43f);
        doneButton.GetComponent<Image>().color = new Color(0.82f, 0.82f, 0.43f);
    }
    public void clickprogress()
    {
        panelControl(false, true, false);
        Click = true;
        ingButton.GetComponent<Image>().color = new Color(0.58f, 0.58f, 0.4f);
        initButton.GetComponent<Image>().color = new Color(0.82f, 0.82f, 0.43f);
        doneButton.GetComponent<Image>().color = new Color(0.82f, 0.82f, 0.43f);
    }
    public void clickdone()
    {
        panelControl(false, false, true);
        Click = true;
        doneButton.GetComponent<Image>().color = new Color(0.58f, 0.58f, 0.4f);
        initButton.GetComponent<Image>().color = new Color(0.82f, 0.82f, 0.43f);
        ingButton.GetComponent<Image>().color = new Color(0.82f, 0.82f, 0.43f);
    }

    public void OpenContent()
    {
        contentUI.SetActive(true);
        contentUI.transform.SetAsLastSibling();
    }
}
