using UnityEngine;
using TMPro;

public class PlayerPickup : MonoBehaviour
{
    private bool hasCube = false;

    [Header("UI")]
    public TextMeshProUGUI finishText;   // UI tekst bij finish

    private void Start()
    {
        // Zorg dat de finish tekst onzichtbaar is
        finishText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cube oppakken
        if (other.CompareTag("Pickup") && !hasCube)
        {
            hasCube = true;
            Destroy(other.gameObject);

            // Stop pickup timer en start de totale timer
            StopwatchTimerWithPickupAdjusted stopwatch = FindObjectOfType<StopwatchTimerWithPickupAdjusted>();
            if (stopwatch != null)
            {
                stopwatch.OnCubePickedUp();
            }
        }

        // Terug naar plane (finish)
        if (other.CompareTag("Finish") && hasCube)
        {
            // Toon finish UI tekst
            finishText.gameObject.SetActive(true);
            finishText.text = "Laptop succesfully retrieved!";

            // Stop de totale timer
            StopwatchTimerWithPickupAdjusted stopwatch = FindObjectOfType<StopwatchTimerWithPickupAdjusted>();
            if (stopwatch != null)
            {
                stopwatch.OnFinishReached();
            }
        }
    }
}
