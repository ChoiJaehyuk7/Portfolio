using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public Camera main;
    Vector3 camPosition;

    [SerializeField] [Range(0.01f, 0.1f)] float shakeRange = 0.05f;
    // Start is called before the first frame update
    public void Shake(float time)
    {
        camPosition = main.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", time);
    }

    void StartShake()
    {
        float camPosX = Random.Range(-main.transform.position.x * 0.001f, main.transform.position.x * 0.001f);
        float camPosY = Random.Range(-main.transform.position.y * 0.001f, main.transform.position.y * 0.001f);
        float camPosZ = Random.Range(-main.transform.position.z * 0.001f, main.transform.position.z * 0.001f);
        main.transform.position = new Vector3(main.transform.position.x + camPosX, main.transform.position.y + camPosY, main.transform.position.z + camPosZ);
    }

    void StopShake()
    {
        CancelInvoke("StartShake");
        main.transform.position = camPosition;
    }
}
