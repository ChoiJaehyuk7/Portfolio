using UnityEngine;

public class CamPos : MonoBehaviour
{
    public float a,b,ca,cb;

    public GameObject player;
    private void Start()
    {
        Vector3 PlayerPos2 = new Vector3(player.transform.position.x + (-13), player.transform.position.y + 18,
         player.transform.position.z);
        this.transform.rotation = Quaternion.Euler(55, 90, 0);
        this.transform.position = Vector3.Lerp(this.transform.position, PlayerPos2, Time.deltaTime * 4);
    }
    void Update()
    {
        Vector3 PlayerPos = new Vector3(player.transform.position.x, player.transform.position.y + 15,
            player.transform.position.z - 10);

        Vector3 PlayerPos2 = new Vector3(player.transform.position.x + ca, player.transform.position.y + cb,
           player.transform.position.z);
        this.transform.rotation = Quaternion.Euler(a, b, 0);
        this.transform.position = Vector3.Lerp(this.transform.position, PlayerPos2, Time.deltaTime * 4);
        //this.transform.position = Vector3.Lerp(this.transform.position, PlayerPos, Time.deltaTime * 4);
    }

}
