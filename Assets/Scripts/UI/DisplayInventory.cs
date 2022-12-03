using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{

    public int X_Start;
    public int Y_Start;
    public InventoryObject inventory;
    public int X_Space_Between_Items;
    public int ColumnNumber;
    public int Y_Space_Between_Items;
    public Dictionary<InventorySlot,GameObject> itemsDisplayed=new Dictionary<InventorySlot, GameObject>();
    public ItemObject selectedItem=null;
    public Button useButton;


    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
        gameObject.SetActive(false);
        useButton.onClick.AddListener(SubmitUse);
        
    }

    // Update is called once per frame

    public void CreateDisplay()
    {
            for(int i=0; i<inventory.Container.Count;i++)
            {
                var obj =Instantiate(inventory.Container[i].item.inventoryPrefab,Vector3.zero,Quaternion.identity,transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.Container[i],obj);
                obj.GetComponent<ItemDisplayed>().displayInventory = this;
                obj.GetComponent<ItemDisplayed>().item = inventory.Container[i].item;
            }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_Start + (X_Space_Between_Items *(i % ColumnNumber)),Y_Start+((-Y_Space_Between_Items*(i/ColumnNumber))),0f);
    }
    public void UpdateDisplay()
    {
        for(int i=0; i<inventory.Container.Count;i++)
        {
            if(itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
    
            }
            else
            {
                var obj =Instantiate(inventory.Container[i].item.inventoryPrefab,Vector3.zero,Quaternion.identity,transform);
                
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.Container[i],obj);
            }
        }
        
    }


    public void SubmitUse()
    {
        inventory.UseItem(selectedItem);
    }

    public void HandleInventoryInteraction()
    {
        if(this.isActiveAndEnabled)
        {
            gameObject.SetActive(false);
            
            Cursor.visible=false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale=1;
            selectedItem =null;
        }
        else
        {
            gameObject.SetActive(true);
            Cursor.visible=true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale=0;
            UpdateDisplay();
        }
    }
}
