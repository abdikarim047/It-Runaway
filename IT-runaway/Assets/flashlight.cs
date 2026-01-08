using UnityEngine;

public class flashlight : MonoBehaviour
{
    public GameObject ON;
    public GameObject OFF;
    private bool isOn;



    void Start()
    {
        ON.SetActive(false);
        OFF.SetActive(true);
        isOn = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOn = !isOn;

            if (isOn)
            {
                ON.SetActive(true);
                OFF.SetActive(false);
            }
            else
            {
                ON.SetActive(false);
                OFF.SetActive(true);
            }
        }
    }
}
