using UnityEngine;

public class QuestButtonController : MonoBehaviour
{
    public QuestButton[] initbuttons;
    public QuestButton[] donebuttons;
    public QuestingButton[] ingbuttons;
    public GameObject pnlinit, pnling, pnldone;
    public int id;

    private void Start()
    {
        for(int i=0; i < 20; i++)
        {
            initbuttons[i].gameObject.SetActive(false);
            ingbuttons[i].gameObject.SetActive(false);
            donebuttons[i].gameObject.SetActive(false);

            ingbuttons[i].title = initbuttons[i].title;
            ingbuttons[i].questID = initbuttons[i].questID;

            donebuttons[i].title = initbuttons[i].title;
            donebuttons[i].questID = initbuttons[i].questID;
        }
        openpanel(false, false, false);

    }
    private void Update()
    {
        contentUI();
    }

    void contentUI()
    {
        //init
        if (initbuttons[id].isClick)
        {
            initbuttons[id].titleText.text = initbuttons[id].title;
            initbuttons[id].contentTitleText.text = initbuttons[id].title;
            initbuttons[id].contentText.text = initbuttons[id].content;
            openpanel(true, false, false);
            initbuttons[id].isClick = false;
        }

        //content
        if (ingbuttons[id].isClick)
        {
            ingbuttons[id].titleText.text = ingbuttons[id].title;
            ingbuttons[id].contentTitleText.text = ingbuttons[id].title;
            ingbuttons[id].contentText.text = ingbuttons[id].content;
            ingbuttons[id].goalText.text = ingbuttons[id].goalstring;
            ingbuttons[id].goalValueText.text = ingbuttons[id].goalValue;
            openpanel(false, true, false);
            ingbuttons[id].isClick = false;
        }

        //done
        if (donebuttons[id].isClick)
        {
            donebuttons[id].titleText.text = donebuttons[id].title;
            donebuttons[id].contentTitleText.text = donebuttons[id].title;
            donebuttons[id].contentText.text = donebuttons[id].content;
            donebuttons[id].goalText.text = ingbuttons[id].goalstring;
            openpanel(false, false, true);
            donebuttons[id].isClick = false;
        }
    }
    void openpanel(bool a, bool b, bool c)
    {
        pnlinit.SetActive(a);
        pnling.SetActive(b);
        pnldone.SetActive(c);
    }
}
