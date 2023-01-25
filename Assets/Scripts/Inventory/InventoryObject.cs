using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheNecromancers.StateMachine.Player;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[CreateAssetMenu(fileName = "New Inventory Object", menuName = "Inventory System/Inventory")]

public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver

{
    public string savePath;
    private ItemDatabaseObject database;
    public List<InventorySlot> Container = new();
    public PlayerStateMachine playerStateMachine { get; set; }

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/ItemsDatabase.asset",
            typeof(ItemDatabaseObject));
#else
    database = Resources.Load<ItemDatabaseObject>("Database");
#endif
    }
    public int IsInInventory(ItemObject _item)
    {   
        int ItemIndexInContainer = new int();

        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                ItemIndexInContainer = i;
                break;
            }
            else
            {
                ItemIndexInContainer = -1 ;
            }
        }

        return ItemIndexInContainer;
    }

    public void DeleteInventoryContainer(int _ContainerIndex)
    {
        Container.RemoveAt(_ContainerIndex);
    }
    public void ClearInventory()
    {
        Container.Clear();
    }

    public void AddItem(ItemObject _item, int _amount)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
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
        BinaryFormatter bf = new();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    public void OnAfterDeserialize()
    {
        if (File.Exists(savePath))
        {
            for (int i = 0; i < Container.Count; i++)
            {
                Container[i].item = database.GetItem[Container[i].ID];
            }
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
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
