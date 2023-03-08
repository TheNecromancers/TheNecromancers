using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemDisplayed : MonoBehaviour
{
    public ItemObject item { get; set; }
    public DisplayInventory displayInventory { get; set; }

    InventoryManager inventoryManager;

    Button itemButton;

    private void Awake() 
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        itemButton = gameObject.GetComponent<Button>();
        itemButton.onClick.AddListener(()=> inventoryManager.UseItem(item));
    }

    private void Update() 
    {

    }
}
