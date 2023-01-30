using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class InventoryBackUp : MonoBehaviour
{
    public InventoryObject BackUp;
    public List<InventorySlot> InventorySlots;
    public string savePath;

    private void Awake()
    {
#if UNITY_EDITOR
        BackUp = (InventoryObject)AssetDatabase.LoadAssetAtPath("Assets/Scripts/SO_Scriptable Objects/InventorySO/Resources/Empty Inventory.asset",
                typeof(InventoryObject));
#else
        BackUp = Resources.Load<InventoryObject>("Empty Inventory");
#endif
        Load();

        if (InventorySlots.Count > 0)
        {
            for (int i = 0; i < InventorySlots.Count; i++)
            {
                BackUp.Container.Add(InventorySlots[i]);
                print(InventorySlots[i]);
            }

            InventorySlots.Clear();
        }
        Save();
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new();
        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Data")))
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Data"));
        }
        FileStream file = File.Create(string.Concat(string.Concat(Application.persistentDataPath, "/Data"), savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(string.Concat(Application.persistentDataPath, "/Data"), savePath)))
        {
            BinaryFormatter bf = new();
            FileStream file = File.Open(string.Concat(string.Concat(Application.persistentDataPath, "/Data"), savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    private void OnApplicationQuit()
    {
        //BackUp.Container.CopyTo(InventorySlots);

        InventorySlots = BackUp.Container.GetRange(0, BackUp.Container.Count);
        BackUp.Container.Clear();
        Save();
    }

}
