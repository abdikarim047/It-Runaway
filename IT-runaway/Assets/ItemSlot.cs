using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image icon; 

    public void SetItem(Sprite sprite)
    {
        icon.sprite = sprite;
        icon.enabled = true;
    }

    public void Clear()
    {
        icon.sprite = null;
        icon.enabled = false;
    }
}
