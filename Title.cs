using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject UIs;
    public GameObject title;
    public GameObject titleCam;
    public GameObject MainCam, cam1, cam2;
    public float timer;
    public float aa;
    bool isStart;
    bool a;
    // Start is called before the first frame update
    void Start()
    {
        MainCam.SetActive(false);
        cam1.SetActive(false);
        cam2.SetActive(false);
        UIs.SetActive(false);
    }

    public void ClickStart()
    {
        isStart = true;
        MainCam.SetActive(true);
        cam1.SetActive(true);
        cam2.SetActive(true);
        titleCam.SetActive(false);
        title.SetActive(false);
        UIs.SetActive(true);
    }
}
