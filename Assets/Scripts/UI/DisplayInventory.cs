using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TheNecromancers.StateMachine.Player;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory; // should be private

    public Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>(); // should be private
    public ItemObject selectedItem = null; // should be private


    [SerializeField] PlayerStateMachine PlayerStateMachine;
    [SerializeField] InventoryManager InventoryManager;
    [SerializeField] GameObject InventoryCamera;
    [SerializeField] GameObject InventoryCanvas;
    [SerializeField] GameObject ItemInventory;
    [SerializeField] GameObject Scrollsinventory;
    [SerializeField] Button UseButton;
    [SerializeField] Button InventoryButton;
    [SerializeField] Button ScrollsButton;


    private void Awake()
    {
        inventory = PlayerStateMachine.inventoryObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        InventoryCanvas.SetActive(false);
        InventoryCamera.SetActive(false);
        CreateDisplay();
        UseButton.onClick.AddListener(() => InventoryManager.ItemSelectionDelegate(selectedItem));
        InventoryButton.onClick.AddListener(ShowInventoryDisplay);
        ScrollsButton.onClick.AddListener(ShowScrollsDisplay);

    }

    // Update is called once per frame

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if(inventory.Container[i].item.type == ItemType.Equipment)
            {
                GameObject obj = Instantiate(inventory.Container[i].item.inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.Container[i], obj);
                obj.GetComponent<ItemDisplayed>().displayInventory = this;
                obj.GetComponent<ItemDisplayed>().item = inventory.Container[i].item;
            }
            else
            {
                GameObject obj = Instantiate(inventory.Container[i].item.inventoryPrefab, Vector3.zero, Quaternion.identity, Scrollsinventory.transform);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.Container[i], obj);
                obj.GetComponent<ItemDisplayed>().displayInventory = this;
                obj.GetComponent<ItemDisplayed>().item = inventory.Container[i].item;
            }

        }
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");

            }
            else
            {
                if(inventory.Container[i].item.type == ItemType.Equipment)
                {
                    var obj = Instantiate(inventory.Container[i].item.inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    itemsDisplayed.Add(inventory.Container[i], obj);
                    obj.GetComponent<ItemDisplayed>().displayInventory = this;
                    obj.GetComponent<ItemDisplayed>().item = inventory.Container[i].item;
                }
                else
                {
                    var obj = Instantiate(inventory.Container[i].item.inventoryPrefab, Vector3.zero, Quaternion.identity, Scrollsinventory.transform);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    itemsDisplayed.Add(inventory.Container[i], obj);
                    obj.GetComponent<ItemDisplayed>().displayInventory = this;
                    obj.GetComponent<ItemDisplayed>().item = inventory.Container[i].item;
                }
            }
        }

    }

    public void GetItem(ItemObject _item)
    {
        selectedItem = _item;
    }

    public void HandleInventoryInteraction()
    {
        if (InventoryCanvas.activeInHierarchy)
        {
            ShowInventoryDisplay();
            InventoryCanvas.SetActive(false);
            InventoryCamera.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            //Time.timeScale=1;
            selectedItem = null;
        }
        else
        {
            ShowInventoryDisplay();
            InventoryCanvas.SetActive(true);
            InventoryCamera.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //Time.timeScale=0;
            UpdateDisplay();
        }
    }

    public void ShowInventoryDisplay()
    {
        ItemInventory.SetActive(true);
        Scrollsinventory.SetActive(false);
    }
        public void ShowScrollsDisplay()
    {
        ItemInventory.SetActive(false);
        Scrollsinventory.SetActive(true);
    }
}
