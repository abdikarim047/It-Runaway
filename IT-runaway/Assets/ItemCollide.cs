using UnityEngine;

public class ItemCollide : MonoBehaviour
{
    public GameObject collectableItem;
    public GameObject inventory;
    private Inventory inventoryScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryScript = inventory.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            //put in inventory
            //destroy prefab
            inventoryScript.additem(collectableItem.name);
            Debug.Log("item: " + collision.transform.name + " collected");
            
            Destroy(this.gameObject);

        }
    }
}
