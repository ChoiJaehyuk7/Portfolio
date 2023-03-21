using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChange : MonoBehaviour
{
    public GameObject CamMain;
    public GameObject CamSub1;
    public GameObject CamSub2;

    public bool isChangedCam;
    [HideInInspector] public DialogueController dialogue;
    void Start()
    {
        CamSub1.SetActive(false);
        CamSub2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogue.synario[0])
        {
            CamMain.SetActive(false);
            CamSub1.SetActive(true);
            isChangedCam = true;
        }
        else if (dialogue.synario[3])
        {
            CamMain.SetActive(true);
            CamSub1.SetActive(false);
            isChangedCam = false;
        }
        else if (dialogue.synario[5])
        {
            CamMain.SetActive(false);
            CamSub2.SetActive(true);
            isChangedCam = true;
        }
        else if (dialogue.synario[6])
        {
            CamMain.SetActive(true);
            CamSub2.SetActive(false);
            isChangedCam = false;
        }
        else if (dialogue.synario[7])
        {
            CamMain.SetActive(false);
            CamSub2.SetActive(true);
            isChangedCam = true;
        }
        else if (dialogue.synario[9])
        {
            CamMain.SetActive(true);
            CamSub2.SetActive(false);
            isChangedCam = false;
        }
    }
}
