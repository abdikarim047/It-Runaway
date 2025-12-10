using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PlayerPickup : MonoBehaviour
{
    private Inventory inventory;
    private bool hasCube = false;

    [Header("UI")]
    public TextMeshProUGUI finishText;   // UI tekst bij finish

    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();

        if (finishText != null)
        {
            finishText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        Worlditem worlditem ;

    

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
