using UnityEngine;
using TMPro;

public class QuestingButton : MonoBehaviour
{
    public string title;
    [TextArea] public string content;
    [TextArea] public string goalstring;
    public string goalValue;
    public int level;
    public int questID;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentTitleText;
    public TextMeshProUGUI contentText;
    public TextMeshProUGUI goalText;
    public TextMeshProUGUI goalValueText;
    public GameObject ing;
    public GameObject done;
    public bool isClick;
    QuestButtonController qb;
    QuestUI qu;
    private void Start()
    {
        qb = FindObjectOfType<QuestButtonController>();
        qu = FindObjectOfType<QuestUI>();
        titleText.text = title;
    }

    private void Update()
    {
        titleText.text = title;
    }

    public void click()
    {
        qb.id = questID;
        isClick = true;
    }
}
