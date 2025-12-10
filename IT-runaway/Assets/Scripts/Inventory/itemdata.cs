using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Objects/ItemData")]

public class ItemData : ScriptableObject
{
    public int id;
    public string itemname;

    public Sprite icon;

    public GameObject itemprefab;

    public bool stackebla = true;

    public int maxstack = 2;

}