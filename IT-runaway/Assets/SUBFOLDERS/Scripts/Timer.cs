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

    // Cube opgepakt → pickup timer stopt, totale timer start
    public void OnCubePickedUp()
    {
        cubePickedUp = true;
        pickupTimerRunning = false;

        Debug.Log("Speler heeft het blokje gevonden in: " + FormatTime(pickupElapsedTime));

        totalTimerText.text = "00:00";
        totalTimerRunning = true;
    }

    // Finish bereikt → stop totale timer
    public void OnFinishReached()
    {
        totalTimerRunning = false;
        Debug.Log("Totale speeltijd vanaf oppakken: " + FormatTime(totalElapsedTime));
    }

    // **Stop alle timers** (bijvoorbeeld bij enemy aanraking)
    public void StopAllTimers()
    {
        pickupTimerRunning = false;
        totalTimerRunning = false;
    }

    // **Reset alle timers** (bijvoorbeeld bij respawn)
    public void ResetTimers()
    {
        pickupElapsedTime = 0f;
        totalElapsedTime = 0f;
        pickupTimerRunning = true;
        totalTimerRunning = false;
        cubePickedUp = false;

        if (pickupTimerText != null)
            pickupTimerText.text = "00:00";

        if (totalTimerText != null)
            totalTimerText.text = "";
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
