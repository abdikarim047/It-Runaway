using UnityEngine;
using TMPro;

public class StopwatchTimerWithPickupAdjusted : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI pickupTimerText;    // tijd tot oppakken
    public TextMeshProUGUI totalTimerText;     // totale tijd, verschijnt pas na oppakken

    private float totalElapsedTime = 0f;
    private float pickupElapsedTime = 0f;
    private bool totalTimerRunning = false;
    private bool pickupTimerRunning = true;    // start direct
    private bool cubePickedUp = false;

    void Start()
    {
        pickupTimerText.text = "00:00";
        totalTimerText.text = ""; // totaal timer onzichtbaar tot cube opgepakt
    }

    void Update()
    {
        if (pickupTimerRunning && !cubePickedUp)
        {
            pickupElapsedTime += Time.deltaTime;
            UpdateTimerUI(pickupTimerText, pickupElapsedTime);
        }

        if (totalTimerRunning)
        {
            totalElapsedTime += Time.deltaTime;
            UpdateTimerUI(totalTimerText, totalElapsedTime);
        }
    }

    void UpdateTimerUI(TextMeshProUGUI textUI, float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        textUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Wordt aangeroepen als de speler het blokje oppakt
    public void OnCubePickedUp()
    {
        cubePickedUp = true;
        pickupTimerRunning = false;

        Debug.Log("Speler heeft het blokje gevonden in: " + FormatTime(pickupElapsedTime));

        // Start de totale timer nu en laat hem zien
        totalTimerText.text = "00:00"; 
        totalTimerRunning = true;
    }

    // Wordt aangeroepen als de speler bij finish is
    public void OnFinishReached()
    {
        totalTimerRunning = false;
        Debug.Log("Totale speeltijd vanaf oppakken: " + FormatTime(totalElapsedTime));
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
