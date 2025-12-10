using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{
    public SimpleFPSController player;
    public float smoothSpeed = 8f;

    private Slider slider;
    private float smoothValue;

    void Start()
    {
        slider = GetComponent<Slider>();
        smoothValue = 1f; // starts full
    }

    void Update()
    {
        if (player == null) return;

        float target = player.GetStamina01();

        // Smooth animation
        smoothValue = Mathf.Lerp(smoothValue, target, Time.deltaTime * smoothSpeed);

        slider.value = smoothValue;
    }
}
