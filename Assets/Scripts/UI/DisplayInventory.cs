using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TheNecromancers.StateMachine.Player;
using UnityEngine.EventSystems;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory; // should be private

    public Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>(); // should be private
    public ItemObject selectedItem = null; // should be private


    [SerializeField] PlayerStateMachine PlayerStateMachine;
    [SerializeField] InventoryManager InventoryManager;
    [SerializeField] GameObject InventoryCamera;
    [SerializeField] GameObject InventoryContainer;
    [SerializeField] GameObject ItemInventory;
    [SerializeField] GameObject ScrollsInventory;
    [SerializeField] GameObject ScrollsInventoryContent;
    [SerializeField] GameObject ItemDescriptionCanvas;
    [SerializeField] TMP_Text ItemName;
    [SerializeField] TMP_Text ItemDescription;
    [SerializeField] Button UseButton;
    [SerializeField] Button InventoryButton;
    [SerializeField] Button ScrollsButton;
    [SerializeField] Button BackButton;
    [SerializeField] Scrollbar scrollbar;
    List<GameObject> MiscItemInInventory;


    private void Awake()
    {
        inventory = Resources.Load<InventoryObject>("Empty Inventory");
    }

    // Start is called before the first frame update
    void Start()
    {
        MiscItemInInventory = new List<GameObject>();
        InventoryContainer.SetActive(false);
        InventoryCamera.SetActive(false);
        CreateDisplay();
        UseButton.onClick.AddListener(() => InventoryManager.ItemSelectionDelegate(selectedItem));
        InventoryButton.onClick.AddListener(ShowInventoryDisplay);
        ScrollsButton.onClick.AddListener(ShowScrollsDisplay);
        BackButton.onClick.AddListener(ShowScrollsDisplay);

    }

    private void Update() 
    {
        if(InventoryContainer.activeSelf && ItemInventory.activeSelf)
        {
            PreventDeselection(InventoryButton.gameObject);
        }
        else if(InventoryContainer.activeSelf && ScrollsInventory.activeSelf && !ItemDescriptionCanvas.activeSelf)
        {
            if(MiscItemInInventory.Count == 0)
            return;
            else
            PreventDeselection(ScrollsButton.gameObject);
            if(MiscItemInInventory.Contains(EventSystem.current.currentSelectedGameObject))
            {
                AdjustScrollValue(MiscItemInInventory.IndexOf(EventSystem.current.currentSelectedGameObject));
            }
            else
            return;

        }
        else if(InventoryContainer.activeSelf && ItemDescriptionCanvas.activeSelf)
        {
            PreventDeselectionForced(BackButton.gameObject);
        }
        else
        return;
    }


    public void AdjustScrollValue(int _index)
    {
        float tempfloat =0f;
        if(_index == 0)
        {
            scrollbar.value = 1f;
        }
        else if(_index == MiscItemInInventory.Count-1)
        {
            scrollbar.value = 0;
        }
        else
        {
            tempfloat = 1f - (((float)_index +0.1f) / ((float)MiscItemInInventory.Count));
            scrollbar.value = tempfloat;
        }

    }


    public void PreventDeselectionForced(GameObject sel)
    {
        if(EventSystem.current.currentSelectedGameObject != sel)
        {
            EventSystem.current.SetSelectedGameObject(sel);
        }
    }
    public void PreventDeselection(GameObject sel)
    {
            if (!EventSystem.current.currentSelectedGameObject.transform.IsChildOf(gameObject.transform))
            {
                EventSystem.current.SetSelectedGameObject(sel);

                if(EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject != sel)
                {
                    sel = EventSystem.current.currentSelectedGameObject;
                }
                else if(sel != null && EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(sel);
                }
            }



    }
    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if(inventory.Container[i].item.type == ItemType.Equipment)
            {
                GameObject obj = Instantiate(inventory.Container[i].item.inventoryPrefab, Vector3.zero, Quaternion.identity, ItemInventory.transform);
                //obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.Container[i], obj);
                obj.GetComponent<ItemDisplayed>().displayInventory = this;
                obj.GetComponent<ItemDisplayed>().item = inventory.Container[i].item;
            }
            else
            {
                GameObject obj = Instantiate(inventory.Container[i].item.inventoryPrefab, Vector3.zero, Quaternion.identity, ScrollsInventoryContent.transform);
                //obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.Container[i], obj);
                obj.GetComponent<ItemDisplayed>().displayInventory = this;
                obj.GetComponent<ItemDisplayed>().item = inventory.Container[i].item;
                MiscItemInInventory.Add(obj);
            }

        }
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            /* MiscItemInInventory.Clear(); */
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                //itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");

            }
            else
            {
                if(inventory.Container[i].item.type == ItemType.Equipment)
                {
                    var obj = Instantiate(inventory.Container[i].item.inventoryPrefab, Vector3.zero, Quaternion.identity, ItemInventory.transform);
                    //obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    itemsDisplayed.Add(inventory.Container[i], obj);
                    obj.GetComponent<ItemDisplayed>().displayInventory = this;
                    obj.GetComponent<ItemDisplayed>().item = inventory.Container[i].item;
                }
                else
                {
                    var obj = Instantiate(inventory.Container[i].item.inventoryPrefab, Vector3.zero, Quaternion.identity, ScrollsInventoryContent.transform);
                    //obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    itemsDisplayed.Add(inventory.Container[i], obj);
                    obj.GetComponent<ItemDisplayed>().displayInventory = this;
                    obj.GetComponent<ItemDisplayed>().item = inventory.Container[i].item;
                    MiscItemInInventory.Add(obj);
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
        if (InventoryContainer.activeInHierarchy)
        {
            ShowInventoryDisplay();
            InventoryContainer.SetActive(false);
            InventoryCamera.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            //Time.timeScale=1;
            selectedItem = null;
        }
        else
        {
            ShowInventoryDisplay();
            InventoryContainer.SetActive(true);
            InventoryCamera.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //Time.timeScale=0;
            UpdateDisplay();
        }
    }

    public void ShowInventoryDisplay()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(InventoryButton.gameObject);
        PreventDeselection(InventoryButton.gameObject);
        ItemInventory.SetActive(true);
        ScrollsInventory.SetActive(false);
        ItemDescriptionCanvas.SetActive(false);
        BackButton.gameObject.SetActive(false);
        UseButton.gameObject.SetActive(true);
    }
        public void ShowScrollsDisplay()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ScrollsButton.gameObject);
        PreventDeselection(ScrollsButton.gameObject);
        ItemInventory.SetActive(false);
        ScrollsInventory.SetActive(true);
        ItemDescriptionCanvas.SetActive(false);
        BackButton.gameObject.SetActive(false);
        UseButton.gameObject.SetActive(true);
    }

    public void ShowItemDescription(string _ItemName, string _ItemDescription)
    {
        
        ItemDescriptionCanvas.SetActive(true);
        BackButton.gameObject.SetActive(true);
        PreventDeselectionForced(BackButton.gameObject);
        UseButton.gameObject.SetActive(false);
        ItemName.text = _ItemName;
        ItemDescription.text =_ItemDescription;


    }



}
