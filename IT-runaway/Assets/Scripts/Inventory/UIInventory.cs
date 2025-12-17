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
     
    }

}
