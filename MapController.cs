using UnityEngine;
using TMPro;

public class MapController : MonoBehaviour
{
    public GameObject Map;
    public bool activeMap;
    bool isSize;
    bool isClick1, isClick2, isClick3, isClick4, isClick5;
    [HideInInspector] public TextMeshProUGUI text1, text2, text3, text4, text5;
    RectTransform rect;
    private void Start()
    {
        Map.SetActive(false);
    }
    private void Update()
    {
        Open();
        FontControl();
    }
    void Open()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            activeMap = !activeMap;
            Map.SetActive(activeMap);
            Map.transform.SetAsLastSibling();
            isClick1 = false;
            isClick2 = false;
            isClick3 = false;
            isClick4 = false;
            isClick5 = false;
        }
    }

    void FontControl()
    {
        if (isClick1)
        {
            //text1.fontSize = 35;
            if (text1.fontSize > 35)
                isSize = false;
            else if (text1.fontSize <= 30)
                isSize = true;

            if (isSize)
                text1.fontSize += 0.04f;
            else
                text1.fontSize -= 0.04f;

            text2.fontSize = 30;
            text3.fontSize = 30;
            text4.fontSize = 30;
            text5.fontSize = 30;
        }
        else if (isClick2)
        {
            if (text2.fontSize > 35)
                isSize = false;
            else if (text2.fontSize <= 30)
                isSize = true;

            if (isSize)
                text2.fontSize += 0.04f;
            else
                text2.fontSize -= 0.04f;

            text1.fontSize = 30;
            text3.fontSize = 30;
            text4.fontSize = 30;
            text5.fontSize = 30;
        }
        else if (isClick3)
        {
            if (text3.fontSize > 35)
                isSize = false;
            else if (text3.fontSize <= 30)
                isSize = true;

            if (isSize)
                text3.fontSize += 0.04f;
            else
                text3.fontSize -= 0.04f;

            text2.fontSize = 30;
            text1.fontSize = 30;
            text4.fontSize = 30;
            text5.fontSize = 30;
        }
        else if (isClick4)
        {
            if (text4.fontSize > 35)
                isSize = false;
            else if (text4.fontSize <= 30)
                isSize = true;

            if (isSize)
                text4.fontSize += 0.04f;
            else
                text4.fontSize -= 0.04f;

            text2.fontSize = 30;
            text3.fontSize = 30;
            text1.fontSize = 30;
            text5.fontSize = 30;
        }
        else if (isClick5)
        {
            if (text5.fontSize > 35)
                isSize = false;
            else if (text5.fontSize <= 30)
                isSize = true;

            if (isSize)
                text5.fontSize += 0.04f;
            else
                text5.fontSize -= 0.04f;

            text2.fontSize = 30;
            text3.fontSize = 30;
            text4.fontSize = 30;
            text1.fontSize = 30;
        }
        else
        {
            text2.fontSize = 30;
            text3.fontSize = 30;
            text4.fontSize = 30;
            text1.fontSize = 30;
            text5.fontSize = 30;
        }
    }
    public void ClickOpen()
    {
        activeMap = false;
        Map.SetActive(false);
        isClick1 = false;
        isClick2 = false;
        isClick3 = false;
        isClick4 = false;
        isClick5 = false;
    }

    public void Click1()
    {
        if (isClick1)
            isClick1 = false;
        else
            isClick1 = true;
        isClick2 = false;
        isClick3 = false;
        isClick4 = false;
        isClick5 = false;
    }

    public void Click2()
    {
        if (isClick2)
            isClick2 = false;
        else
            isClick2 = true;
        isClick1 = false;
        isClick3 = false;
        isClick4 = false;
        isClick5 = false;
    }
    public void Click3()
    {
        if (isClick3)
            isClick3 = false;
        else
            isClick3 = true;
        isClick1 = false;
        isClick2 = false;
        isClick4 = false;
        isClick5 = false;
    }
    public void Click4()
    {
        if (isClick4)
            isClick4 = false;
        else
            isClick4 = true;
        isClick1 = false;
        isClick2 = false;
        isClick3 = false;
        isClick5 = false;
    }
    public void Click5()
    {
        if (isClick5)
            isClick5 = false;
        else
            isClick5 = true;
        isClick1 = false;
        isClick2 = false;
        isClick3 = false;
        isClick4 = false;
    }
}
