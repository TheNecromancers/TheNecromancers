using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplayed : MonoBehaviour
{
    public ItemObject item { get; set; }
    public DisplayInventory displayInventory { get; set; }
    Button itemButton;

    private void Awake() 
    {
        
        itemButton = gameObject.GetComponent<Button>();
        itemButton.onClick.AddListener(SelectItem);

    }


    public void SelectItem()
    {
        displayInventory.selectedItem = null;
        displayInventory.selectedItem = item;
    }

    private void Update() 
    {

    }
}
