using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillGauge : MonoBehaviour
{
    public Image GaugeImg;
    public float gaugeVal;
    public TextMeshProUGUI gaugeTxt;
    public int gaugeDmg;
    public bool isGauge;
    float a;
    // Start is called before the first frame update
    void Start()
    {
        GaugeImg.fillAmount = gaugeVal;
    }

    // Update is called once per frame
    void Update()
    {
        gaugeTxt.text = ((int)gaugeVal).ToString();
        GaugeImg.fillAmount = 0.01f * gaugeVal;
        if (gaugeVal >= 100)
        {
            gaugeVal = 100;
            if (Input.GetKeyDown(KeyCode.B))
                isGauge = true;
        }
        if (isGauge)
        {
            if (gaugeVal > 0)
            {
                gaugeVal -= Time.deltaTime * 5;
                gaugeDmg = 1;
            }
            else
            {
                gaugeVal = 0;
                gaugeDmg = 0;
                isGauge = false;
            }
        }
    }
}
