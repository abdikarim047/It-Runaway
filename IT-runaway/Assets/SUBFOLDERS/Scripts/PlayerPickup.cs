using UnityEngine;
using TMPro;
using UnityEngine.UI; // needed for Sprite

public class PlayerPickup : MonoBehaviour
{
    private bool hasCube = false;
    public GameObject inventory;
    private Inventory inventoryScript;

    [Header("UI")]
    public TextMeshProUGUI finishText;

    private void Start()
    {
        inventoryScript = inventory.GetComponent<Inventory>();

        if (finishText != null)
            finishText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Pickup") && !hasCube)
        {
            hasCube = true;

 
            laptopitem laptop = other.GetComponent<laptopitem>();
            Sprite icon = null;
            if (laptop != null)
                icon = laptop.laptopIcon;

        
            inventoryScript.additem(other.name, icon);

            Debug.Log("item: " + other.transform.name + " collected");

   
            Destroy(other.gameObject);

            StopwatchTimerWithPickupAdjusted stopwatch = FindObjectOfType<StopwatchTimerWithPickupAdjusted>();
            if (stopwatch != null)
                stopwatch.OnCubePickedUp();

         
            FindObjectOfType<UIInventory>()?.Refresh();
        }

        if (other.CompareTag("Finish") && hasCube)
        {
            if (finishText != null)
            {
                finishText.gameObject.SetActive(true);
                finishText.text = "Laptop succesfully retrieved!";
            }

            StopwatchTimerWithPickupAdjusted stopwatch = FindObjectOfType<StopwatchTimerWithPickupAdjusted>();
            if (stopwatch != null)
                stopwatch.OnFinishReached();
        }
    }

    // 🔁 Respawn reset — old code preserved
    public void ResetPickup()
    {
        hasCube = false;

        if (finishText != null)
            finishText.gameObject.SetActive(false);
    }
}
