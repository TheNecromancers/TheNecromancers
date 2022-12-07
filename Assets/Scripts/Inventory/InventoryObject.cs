using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheNecromancers.StateMachine.Player;
using UnityEditor;

[CreateAssetMenu(fileName ="New Inventory Object", menuName ="Inventory System/Inventory")]

public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    private ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>();
    public PlayerStateMachine playerStateMachine { get; set; }

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", 
            typeof(ItemDatabaseObject));
#else
    database = Resources.Load<ItemDatabaseObject>("Database");

#endif
    }
    public void AddItem(ItemObject _item, int _amount)
    {
        for(int i =0; i < Container.Count; i++)
        {
            if(Container[i].item ==_item)
            {
                Container[i].AddAmount(_amount);
                return;
            }
        }
        Container.Add(new InventorySlot(database.GedId[_item], _item, _amount));
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        // da fare evitare di usare il Binary formatter !

    }

    public void Load()
    {
        // da fare 
    }

    public void OnAfterDeserialize()
    {
        for (int i =0; i < Container.Count;i++) 
        {
            Container[i].item = database.GetItem[Container[i].ID];
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public ItemObject item;
    public int amount;
    public InventorySlot(int _id, ItemObject _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount=_amount;
    }

    public void AddAmount(int value)
    {
        amount+= value;
    }

    
}
