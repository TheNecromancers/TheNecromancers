using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheNecromancers.StateMachine.Player;
using Unity.XR.GoogleVr;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Chest : MonoBehaviour, IInteractable
{
    
    public bool isInteractable = true;
    public bool IsInteractable => isInteractable;
    public string savePath;
    [SerializeField] ItemObject item;
    /*[SerializeField]*/ GameObject InteractiveText;

    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InteractiveText = this.transform.GetChild(0).gameObject;
        Load();
    }

    public void OnInteract()
    {
        if (!isInteractable) return;

        AddItemToInventory(player.GetComponent<PlayerStateMachine>().inventoryObject);
        InteractiveText.SetActive(false);
        print("Interact with " + gameObject.name);

        isInteractable = false;
        Save();
    }

    public void OnStartHover()
    {
        if (!isInteractable) return;

        InteractiveText.SetActive(true);
        print(gameObject.name + " OnStartHover");

    }

    public void OnEndHover()
    {
        if (!isInteractable) return;

        InteractiveText.SetActive(false);
        print(gameObject.name + " OnEndHover");

    }

    void AddItemToInventory(InventoryObject inventory)
    {
        inventory.AddItem(item, 1);
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

    public void ResetChest()
    {
        isInteractable = true;
    }
}
