using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDamage : MonoBehaviour
{
    [Header("UI")]
    public Image gameOverImage;         // Game Over popup

    [Header("Respawn Settings")]
    public Transform respawnPoint;      // Spawnpunt voor respawn
    public float respawnDelay = 5f;     // Seconden voor respawn

    [Header("Pickup Script")]
    public PlayerPickup playerPickup;   // Referentie naar PlayerPickup script

    private bool isDead = false;
    private Renderer playerRenderer;
    private Collider playerCollider;
    private Rigidbody rb;
    private CharacterController cc;

    private void Start()
    {
        if (gameOverImage != null)
            gameOverImage.gameObject.SetActive(false);

        playerRenderer = GetComponent<Renderer>();
        playerCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            isDead = true;

            // Toon Game Over UI
            if (gameOverImage != null)
                gameOverImage.gameObject.SetActive(true);

            // Stop timers
            StopwatchTimerWithPickupAdjusted stopwatch = FindObjectOfType<StopwatchTimerWithPickupAdjusted>();
            if (stopwatch != null)
                stopwatch.StopAllTimers();

            // Disable beweging
            if (cc != null) cc.enabled = false;
            if (rb != null) rb.isKinematic = true;

            // Verberg speler tijdelijk
            if (playerRenderer != null) playerRenderer.enabled = false;
            if (playerCollider != null) playerCollider.enabled = false;

            // Start respawn
            StartCoroutine(RespawnPlayer());
        }
    }

    private IEnumerator RespawnPlayer()
    {
        // Optioneel: speler offscreen zetten
        transform.position = new Vector3(0, -50, 0);

        // Wacht respawnDelay
        yield return new WaitForSeconds(respawnDelay);

        // Teleporteer naar spawnpunt
        if (respawnPoint != null)
            transform.position = respawnPoint.position;

        // Reset spelerstatus
        isDead = false;

        // Reset pickup status als je PlayerPickup koppelt
        if (playerPickup != null)
            playerPickup.ResetPickup(); // moet een ResetPickup() methode hebben

        // Verberg Game Over UI
        if (gameOverImage != null)
            gameOverImage.gameObject.SetActive(false);

        // Reset timers
        StopwatchTimerWithPickupAdjusted stopwatch = FindObjectOfType<StopwatchTimerWithPickupAdjusted>();
        if (stopwatch != null)
            stopwatch.ResetTimers();

        // Zet speler weer actief
        if (playerRenderer != null) playerRenderer.enabled = true;
        if (playerCollider != null) playerCollider.enabled = true;
        if (cc != null) cc.enabled = true;
        if (rb != null) rb.isKinematic = false;
    }
}
