using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Inventorymanager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject InventoryMenu;
    private bool menuActivated = false;

    public KeyCode togglekey = KeyCode.M;

    void Start()
    {
        if (InventoryMenu != null)
        {
            InventoryMenu.SetActive(false);
            menuActivated = false;

        }
        else
        {
            Debug.LogWarning("InventoryManager: inventoryPanel not assigned in Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(togglekey))
        {
            toggleinvetory();
        }


    }
    void toggleinvetory()
    {
        menuActivated = !menuActivated;

        if (InventoryMenu != null)
        {
            InventoryMenu.SetActive(menuActivated);
        }
    }
}
