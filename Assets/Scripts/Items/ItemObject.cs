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
    LeftHand,
    RightHand,

}
[CreateAssetMenu(fileName = "Item", menuName = "Items/New Item", order = 0)]
public class ItemObject : ScriptableObject
{

    public GameObject inventoryPrefab;
    public GameObject itemPrefab;
    public ItemType type;
    
    [TextArea(15,20)]
    public string description;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
