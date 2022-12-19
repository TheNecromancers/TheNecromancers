using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class ItemDisplayed : MonoBehaviour
{
    public ItemObject item { get; set; }
    public DisplayInventory displayInventory { get; set; }
    Button itemButton;

    private void Awake() 
    {
        itemButton = gameObject.GetComponent<Button>();
        itemButton.onClick.AddListener(()=> displayInventory.GetItem(item));
    }

    private void Update() 
    {

    }
}
