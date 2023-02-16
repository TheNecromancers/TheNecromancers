using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheNecromancers.StateMachine.Player;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class PickuppableItem : MonoBehaviour, IInteractable
{
    public bool isInteractable = true;
    public bool IsInteractable => isInteractable;
    [SerializeField] ItemObject item;
    public string ItemName;
    public string savePath;

    public event Action onPickupAction;
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Load();
        if(!isInteractable) gameObject.SetActive(false);
    }

    public void OnInteract()
    {
        if (!isInteractable) return;

        AddItemToInventory(player.GetComponent<PlayerStateMachine>().inventoryObject);
        print("Interact with " + gameObject.name);
        if(onPickupAction != null)  onPickupAction();
        isInteractable = false;
        gameObject.SetActive(false);
        Save();
    }

    public void OnStartHover()
    {
        

        if (!isInteractable) return;
        //remove outline of the object
        Outline outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.OutlineColor = new Color(outline.OutlineColor.r, outline.OutlineColor.g, outline.OutlineColor.b, 1);
        }


        print("questo è il player"+player);
        print("questo è l'inventario del player memorizzato sull'oggetto interagibile "+player.GetComponent<PlayerStateMachine>().inventoryObject);
        print("premi e per raccogliere: "+ item+" Scriptable Object, "+ "oggetto all'interno: "+item.itemPrefab);
        print(gameObject.name + " OnStartHover");

    }

    public void OnEndHover()
    {
        if (!isInteractable) return;
        //remove outline of the object
        Outline outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.OutlineColor = new Color(outline.OutlineColor.r, outline.OutlineColor.g, outline.OutlineColor.b, 0);
        }

        print(gameObject.name + " OnEndHover");

    }

    public void AddItemToInventory(InventoryObject inventory)
    {
        inventory.AddItem(item, 1);
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
        Save();
    }
}
