using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color newColor = Color.red;

    void Start()
    {
        foreach (Transform child in transform)
        {
            Renderer rend = child.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material.color = newColor;
            }
        }
    }
}
