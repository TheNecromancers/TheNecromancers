using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Consumable Object", menuName ="Inventory System/Items/Consumable")]

public class ConsumableObject : ItemObject
{

    public void Awake()
    {
        type = ItemType.Consumable;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
