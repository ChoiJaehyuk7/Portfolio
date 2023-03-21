using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public static ItemDB instance;
    private void Awake()
    {
        instance = this;
    }

    public List<Item> itemDB = new List<Item>();

    private void Start()
    {
        for(int i = 0; i<itemDB.Count; i++)
        {
            itemDB[i].itemID = i;
        }
    }
}
