using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipment,
    Default,
}
public enum WeaponType
{
    Sword,
    Shield,
    Axe
}
[CreateAssetMenu(fileName = "Item", menuName = "Items/New Item", order = 0)]
public class ItemObject : ScriptableObject
{
    public GameObject inventoryPrefab;
    public GameObject itemPrefab;
    public ItemType type;
    public string descriptionTitle;
    
    [TextArea(15,20)]
    public string description;
}
