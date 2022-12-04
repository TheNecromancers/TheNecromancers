using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheNecromancers.StateMachine.Player;
public class InventoryManager : MonoBehaviour

{

    public InventoryObject inventoryObject { get;  set; }
    public PlayerStateMachine playerStateMachine { get;  set; }
    public DisplayInventory displayInventory { get;  set; }



    public void UseItem(ItemObject _item)
        {
            if(_item is WeaponSO)
            {   
                
                WeaponSO _tempSO = (WeaponSO)_item;
                Debug.Log(_tempSO.itemPrefab);

                Equip(_tempSO);

            }
        }
    public void Equip(WeaponSO weapon)
    {   

        //inventoryObject.AddItem(weapon,-1);
        playerStateMachine.OnWeaponChanged(weapon);

        if(weapon.WeaponType == WeaponType.LeftHand)
        {
            //inventoryObject.AddItem(playerStateMachine.WeaponLeftHand,1);
            Destroy(playerStateMachine.LeftHandHolder.transform.GetChild(0).gameObject);
            Instantiate(weapon.itemPrefab, playerStateMachine.LeftHandHolder.transform);

            
            
        }
        if(weapon.WeaponType == WeaponType.RightHand)
        {
            //inventoryObject.AddItem(playerStateMachine.WeaponRightHand,1);
            Destroy(playerStateMachine.RightHandHolder.transform.GetChild(0).gameObject);
            Instantiate(weapon.itemPrefab, playerStateMachine.RightHandHolder.transform);


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
