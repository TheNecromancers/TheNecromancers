using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheNecromancers.StateMachine.Player;

[CreateAssetMenu(fileName ="New Inventory Object", menuName ="Inventory System/Inventory")]

public class InventoryObject : ScriptableObject
{

    public List<InventorySlot> Container = new List<InventorySlot>();
    public PlayerStateMachine playerStateMachine { get; set; }
    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        for(int i =0; i<Container.Count; i++)
        {
            if(Container[i].item ==_item)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if(!hasItem)
        {
            Container.Add(new InventorySlot(_item, _amount));
        }
    }
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
        AddItem(weapon,-1);

        if(weapon.WeaponType == WeaponType.LeftHand)
        {
            Instantiate(weapon.itemPrefab, playerStateMachine.LeftHandHolder.transform);
            AddItem(playerStateMachine.WeaponLeftHand,1);
            
        }
        if(weapon.WeaponType == WeaponType.RightHand)
        {
            Instantiate(weapon.itemPrefab, playerStateMachine.RightHandHolder.transform);
            AddItem(playerStateMachine.WeaponRightHand,1);

        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount=_amount;
    }

    public void AddAmount(int value)
    {
        amount+= value;
    }

    
}
