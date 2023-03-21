using UnityEngine;
using TMPro;

public enum questType
{
    init,
    done
}

public class QuestButton : MonoBehaviour
{
    public questType type;
    public string title;
    [TextArea] public string content;
    public int level;
    public int questID;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentTitleText;
    public TextMeshProUGUI contentText;
    public TextMeshProUGUI goalText;
    public bool isClick;
    QuestButtonController qb;
    QuestUI qu;
    private void Start()
    {
        qb = FindObjectOfType<QuestButtonController>();
        qu = FindObjectOfType<QuestUI>();
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
