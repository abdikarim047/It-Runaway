using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventory : MonoBehaviour
{
    public Inventory inventory;
    public Transform Slotparent;

    private ItemSlot[] slots;

    void Start()
    {
        slots = Slotparent.GetComponentsInChildren<ItemSlot>();
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                // Set the sprite in the slot
                slots[i].SetItem(inventory.items[i].icon);
            }
            else
            {
                // Clear extra slots
                slots[i].Clear();
            }
        }
    }
}
