using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public string name;
        public Sprite icon;
    }

    public List<InventoryItem> items = new List<InventoryItem>();

    public void additem(string itemName, Sprite itemIcon)
    {
        items.Add(new InventoryItem { name = itemName, icon = itemIcon });
        Debug.Log("Added: " + itemName);
    }
}
