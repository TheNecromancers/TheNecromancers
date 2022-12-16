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
        if (_item is WeaponSO)
        {
            WeaponSO _tempSO = (WeaponSO)_item;
            Debug.Log(_tempSO.itemPrefab);

            Equip(_tempSO);
        }
    }
    public void Equip(WeaponSO weapon)
    {
        //inventoryObject.AddItem(weapon,-1);

        if (weapon.WeaponType == WeaponType.LeftHand)
        {
            //inventoryObject.AddItem(playerStateMachine.WeaponLeftHand,1);
            if (playerStateMachine.LeftHandHolder.transform.childCount > 0)
            {
                Destroy(playerStateMachine.LeftHandHolder.transform.GetChild(0).gameObject);
            }

            GameObject _newWeapon = Instantiate(weapon.itemPrefab, playerStateMachine.LeftHandHolder.transform);
            playerStateMachine.WeaponLeftHand = weapon;
            Debug.Log("equipped" + weapon + "on left hand");
        }

        if (weapon.WeaponType == WeaponType.RightHand)
        {
            //inventoryObject.AddItem(playerStateMachine.WeaponRightHand, 1);

            // switch weapons instead of destroy its
            if(playerStateMachine.RightHandHolder.transform.childCount > 0)
            {
                Destroy(playerStateMachine.RightHandHolder.transform.GetChild(0).gameObject);
            }

            GameObject _newWeapon = Instantiate(weapon.itemPrefab, playerStateMachine.RightHandHolder.transform);
            playerStateMachine.WeaponRightHand = weapon;
            playerStateMachine.WeaponLogic = _newWeapon.GetComponent<WeaponLogic>();
            Debug.Log("equipped" + weapon + "on right hand");
        }

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
