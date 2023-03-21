using UnityEngine;
using TMPro;

public class DamageTextController : MonoBehaviour
{
    public TextMeshPro text;
    PlayerTrigger pt;
    public int dmgTextID;
    public int damage;
    public string miss;
    public string level;
    // Start is called before the first frame update
    void Start()
    {
        pt = FindObjectOfType<PlayerTrigger>();
        text = GetComponent<TextMeshPro>();
        if (dmgTextID == 2)
        {
            text.text = level;
        }
        else if(dmgTextID == 1)
        {
            if (!pt.isBuff)
                text.text = damage.ToString();
            else
                text.text = miss;
        }
        else
        {
            text.text = damage.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {   
        DeleteDmgText();
    }

    void DeleteDmgText()
    {
        Destroy(this.gameObject, 1f);
        if (dmgTextID == 0)
            this.transform.Translate(0, 0.01f, 0);
    }
}
