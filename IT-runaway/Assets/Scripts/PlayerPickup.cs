using UnityEngine;
using TMPro;

public class PlayerPickup : MonoBehaviour
{
    private bool hasCube = false;
    public GameObject inventory;
    private Inventory inventoryScript;

    [Header("UI")]
    public TextMeshProUGUI finishText;   // UI tekst bij finish

    private void Start()
    {
        inventoryScript = inventory.GetComponent<Inventory>();
        // Zorg dat finish tekst standaard uit staat
        if (finishText != null)
            finishText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cube oppakken
        if (other.CompareTag("Pickup") && !hasCube)
        {
            hasCube = true;
            Destroy(other.gameObject);
            inventoryScript.additem(other.name);
            Debug.Log("item: " + other.transform.name + " collected");

            // Stop pickup timer en start totale timer
            StopwatchTimerWithPickupAdjusted stopwatch = FindObjectOfType<StopwatchTimerWithPickupAdjusted>();
            if (stopwatch != null)
                stopwatch.OnCubePickedUp();
        }

        // Finish bereiken
        if (other.CompareTag("Finish") && hasCube)
        {
            if (finishText != null)
            {
                finishText.gameObject.SetActive(true);
                finishText.text = "Laptop succesfully retrieved!";
            }

            // Stop de totale timer
            StopwatchTimerWithPickupAdjusted stopwatch = FindObjectOfType<StopwatchTimerWithPickupAdjusted>();
            if (stopwatch != null)
                stopwatch.OnFinishReached();
        }
    }

    // Methode voor PlayerDamage om de pickup status te resetten bij respawn
    public void ResetPickup()
    {
        hasCube = false;

        if (finishText != null)
            finishText.gameObject.SetActive(false);
    }
}
