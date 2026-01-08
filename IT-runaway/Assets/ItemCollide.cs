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
            // Get the sprite from the prefab
            SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Sprite itemSprite = sr.sprite;
                //put in inventory
                //inventoryScript.additem();
            }
            //destroy prefab
            Debug.Log("item: " + collision.transform.name + " collected");

            Destroy(this.gameObject);

        }
    }
}
