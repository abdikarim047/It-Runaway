using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Invantory/Item")]

public class ItemData : ScriptableObject
{
    public string id;
    public string itemname;

    public Sprite icon;

    public GameObject itemprefab;

    public bool stackebla = true;

    public int maxstack = 2;
}