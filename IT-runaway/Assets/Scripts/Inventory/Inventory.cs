using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] 
    private List<ItemData> items = new List<ItemData>();
    
    public void additem(ItemData item)
    {
        items.Add(item);
        Debug.Log("added:" + item.itemname);
    }

    public void removeItem(ItemData item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log("remove" + item.itemname);
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
