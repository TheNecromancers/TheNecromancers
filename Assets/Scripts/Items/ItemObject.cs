using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Consumable,
    Equipment,
    Default,
}
public abstract class ItemObject : ScriptableObject
{

    public GameObject inventoryPrefab;
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
