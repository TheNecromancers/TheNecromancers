using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheNecromancers.StateMachine.Player;
using TheNecromancers.Combat;

public class InventoryManager : MonoBehaviour
{
    public InventoryObject inventoryObject { get; set; }
    public PlayerStateMachine playerStateMachine { get; set; }
    public DisplayInventory displayInventory { get; set; }

    public delegate void SlotSlectionDelegate(ItemObject _item);
    public SlotSlectionDelegate ItemSelectionDelegate;

    private void Awake() 
    {
        inventoryObject = Resources.Load<InventoryObject>("Empty Inventory");
    }
    private void OnEnable()
    {
        ItemSelectionDelegate += UseItem;
    }
    private void OnDisable()
    {
        ItemSelectionDelegate -= UseItem;
    }
    public void UseItem(ItemObject _item)
    {
        if(_item == null)
        return;
        else
        {
            if (_item is WeaponSO)
            {
                WeaponSO _tempSO = (WeaponSO)_item;
                Debug.Log(_tempSO.itemPrefab);
    
                Equip(_tempSO);
            }
            else if(_item.type == ItemType.Default)
            {
                displayInventory.ShowItemDescription(_item.descriptionTitle,_item.description);
            }
        }
    }
    public void Equip(WeaponSO weapon)
    {
        //inventoryObject.AddItem(weapon,-1);

        if (weapon.WeaponType == WeaponType.Shield)
        {
            if (playerStateMachine.WeaponRightHand.WeaponType == WeaponType.Axe) return;

            //inventoryObject.AddItem(playerStateMachine.WeaponLeftHand,1);
            if (playerStateMachine.LeftHandHolder.transform.childCount == 0)
            {
                GameObject _newWeapon = Instantiate(weapon.itemPrefab, playerStateMachine.LeftHandHolder.transform);
                playerStateMachine.WeaponLeftHand = weapon;
                Debug.Log("equipped" + weapon + "on left hand");
            }

            else if(weapon == playerStateMachine.WeaponLeftHand)
            {
                return;
            }

            else if(weapon != playerStateMachine.WeaponLeftHand && inventoryObject.IsInInventory(playerStateMachine.WeaponLeftHand) != -1)
            {
                Destroy(playerStateMachine.LeftHandHolder.transform.GetChild(0).gameObject);
                GameObject _newWeapon = Instantiate(weapon.itemPrefab, playerStateMachine.LeftHandHolder.transform);
                playerStateMachine.WeaponLeftHand = weapon;
                Debug.Log("equipped" + weapon + "on left hand");
            }    

            else if (weapon != playerStateMachine.WeaponLeftHand && inventoryObject.IsInInventory(playerStateMachine.WeaponLeftHand) == -1)
            {
                inventoryObject.AddItem(playerStateMachine.WeaponLeftHand,1);
                Destroy(playerStateMachine.LeftHandHolder.transform.GetChild(0).gameObject);
                GameObject _newWeapon = Instantiate(weapon.itemPrefab, playerStateMachine.LeftHandHolder.transform);
                playerStateMachine.WeaponLeftHand = weapon;
                Debug.Log("equipped" + weapon + "on left hand");
            }
            


        }

        if (weapon.WeaponType == WeaponType.Sword || weapon.WeaponType == WeaponType.Axe)
        {
            //inventoryObject.AddItem(playerStateMachine.WeaponRightHand, 1);
            if(weapon.WeaponType == WeaponType.Axe)
            {
                if(playerStateMachine.LeftHandHolder.transform.childCount > 0)
                {
                    Destroy(playerStateMachine.LeftHandHolder.transform.GetChild(0).gameObject);
                }
                //Unequip(); not implemented
            }

            // switch weapons instead of destroy its

                if(playerStateMachine.RightHandHolder.transform.childCount == 0)
                {
                    GameObject _newWeapon = Instantiate(weapon.itemPrefab, playerStateMachine.RightHandHolder.transform);
                    playerStateMachine.WeaponRightHand = weapon;
                    playerStateMachine.WeaponLogic = _newWeapon.GetComponent<WeaponLogic>();
                    Debug.Log("equipped" + weapon + "on right hand");
                }
                
                else if(weapon == playerStateMachine.WeaponRightHand)
                {
                    return;
                }

                else if(weapon != playerStateMachine.WeaponRightHand && inventoryObject.IsInInventory(playerStateMachine.WeaponRightHand) != -1)
                {
                    Destroy(playerStateMachine.RightHandHolder.transform.GetChild(0).gameObject);
                    GameObject _newWeapon = Instantiate(weapon.itemPrefab, playerStateMachine.RightHandHolder.transform);
                    playerStateMachine.WeaponRightHand = weapon;
                    playerStateMachine.WeaponLogic = _newWeapon.GetComponent<WeaponLogic>();
                    Debug.Log("equipped" + weapon + "on right hand");
                }
                
                else if (weapon != playerStateMachine.WeaponRightHand && inventoryObject.IsInInventory(playerStateMachine.WeaponRightHand) == -1)
                {
                    inventoryObject.AddItem(playerStateMachine.WeaponRightHand,1);
                    Destroy(playerStateMachine.RightHandHolder.transform.GetChild(0).gameObject);
                    GameObject _newWeapon = Instantiate(weapon.itemPrefab, playerStateMachine.RightHandHolder.transform);
                    playerStateMachine.WeaponRightHand = weapon;
                    playerStateMachine.WeaponLogic = _newWeapon.GetComponent<WeaponLogic>();
                    Debug.Log("equipped" + weapon + "on right hand");
                }

                
                
                
            }

            /* GameObject _newWeapon = Instantiate(weapon.itemPrefab, playerStateMachine.RightHandHolder.transform);
            playerStateMachine.WeaponRightHand = weapon;
            playerStateMachine.WeaponLogic = _newWeapon.GetComponent<WeaponLogic>();
            Debug.Log("equipped" + weapon + "on right hand"); */
        

    }
    public void Unequip()
    {

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
