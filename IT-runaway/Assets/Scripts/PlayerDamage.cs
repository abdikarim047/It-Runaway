using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public Image gameOverImage; // sleep hier je GameOverImage in

    private void Start()
    {
        // Zet standaard uit
        gameOverImage.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameOverImage.gameObject.SetActive(true);
        }
    }
}
