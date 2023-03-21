using System.Collections;
using UnityEngine;
using Random = System.Random;

public class RandomSeed : MonoBehaviour
{
    Random random1;
    public int randValue;
    public float randValue2;

    public float percent;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        random1 = new Random((int)Time.realtimeSinceStartup * 1000);
        for (int i = 0; i < 20000; i++)
        {
            yield return new WaitForSeconds(0.5f);
            randValue = random1.Next();
            randValue2 = Time.deltaTime;

            percent = (randValue * randValue2 * 0.00001f);

            if(percent > 100)
                percent = percent - 100;
        }
    }

}
