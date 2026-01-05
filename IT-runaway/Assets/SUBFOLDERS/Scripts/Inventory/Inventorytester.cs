using UnityEngine;

public class Inventorytester : MonoBehaviour
{
    public Inventory inventory;
    public ItemData item1;
    public ItemData item2;

    private void Start()
    {

        inventory.additem(item1);
        inventory.additem(item2);


        inventory.PrintInventory();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

