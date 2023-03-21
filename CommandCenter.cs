using UnityEngine;
public class CommandCenter : MonoBehaviour
{
    GameObject player;
    [HideInInspector] public Vector3 SaveVec;
    public bool IsCritical;
    public bool IsSustain;
    
    public int dmgCC;
    public int dmgSs;
    public GameObject DmgText;
    public GameObject DmgdText;
    public GameObject MissText;
    public GameObject LevelText;
    public GameObject CriticalDmgText;

    private void Start()
    {
        player = GameObject.Find("Character");
    }
    public void DamagedText(int dmg)
    {
        GameObject DText = Instantiate(DmgdText, new Vector3(
           SaveVec.x,
           SaveVec.y + 2f,
           SaveVec.z), Quaternion.Euler(35, 90, 0));
        DText.GetComponent<DamageTextController>().damage = dmg;
    }

    public void DamagedMissText(string txt)
    {
        GameObject DText = Instantiate(MissText, new Vector3(
           SaveVec.x,
           SaveVec.y + 2f,
           SaveVec.z), Quaternion.Euler(35, 90, 0));
        DText.GetComponent<DamageTextController>().miss = txt;
    }

    public void LevelUpText(string txt)
    {
        GameObject DText = Instantiate(LevelText, new Vector3(
           player.transform.position.x,
           player.transform.position.y + 1.5f,
           player.transform.position.z), Quaternion.Euler(35, 90, 0));
        DText.GetComponent<DamageTextController>().level = txt;
    }
}
