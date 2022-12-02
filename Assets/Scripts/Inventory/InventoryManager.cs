using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheNecromancers.StateMachine.Player;
public class InventoryManager : MonoBehaviour

{

    [field: SerializeField] public InventoryObject inventoryObject { get; private set; }
    [field: SerializeField] public PlayerStateMachine playerStateMachine { get; private set; }
    DisplayInventory displayInventory;


    public void Equip(WeaponSO weapon)
    {   
        inventoryObject.AddItem(weapon,-1);

        if(weapon.WeaponType == WeaponType.LeftHand)
        {
            Instantiate(weapon.WeaponPrefab, playerStateMachine.LeftHandHolder.transform);
            inventoryObject.AddItem(playerStateMachine.WeaponLeftHand,1);
            
        }
        if(weapon.WeaponType == WeaponType.RightHand)
        {
            Instantiate(weapon.WeaponPrefab, playerStateMachine.RightHandHolder.transform);
            inventoryObject.AddItem(playerStateMachine.WeaponRightHand,1);

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
