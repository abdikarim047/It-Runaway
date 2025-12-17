using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public List<string> items = new List<string>();

    public void additem(string item)
    {
        items.Add(item);
        Debug.Log("added:" + item);
    }

    // public void removeItem(ItemData item)
    // {
    //     if (items.Contains(item))
    //     {
    //         items.Remove(item);
    //         Debug.Log("remove" + item.itemname);
    //     }
    // }

    public void PrintInventory()
    {
        Debug.Log("Inventory:");

        foreach (string item in items)
        {
            Debug.Log(item);
        }

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
