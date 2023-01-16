using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TheNecromancers.Combat;
using UnityEngine;

public class Torch : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject Light;
    [SerializeField] GameObject ConsumeText;

    public bool isConsumable = false;
    public bool isInteractable = true;
    public bool IsInteractable => isInteractable;
    public string savePath;

    private void Awake()
    {
        Load();

        if ((!isInteractable && isConsumable) ^ (isInteractable && isConsumable))
        {
            Light.SetActive(true);
        }
    }

    public void OnStartHover()
    {
        if (!isInteractable && !isConsumable) return;
        if (isInteractable && !isConsumable)
        {
            print(gameObject.name + " OnStartHover");
        }
        else if (isInteractable && isConsumable)
        {
            //ConsumeText.SetActive(true);
            print(gameObject.name + " OnStartHover");
        }
    }
    public void OnInteract()
    {
        if (!isInteractable && !isConsumable) return;
        if (isInteractable && !isConsumable)
        {
            isConsumable = true;
            Light.SetActive(true);
            print("Interact with" + gameObject.name);
        }
        else if (isInteractable && isConsumable)
        {
            isInteractable = false;
            isConsumable = false;
            Light.SetActive(false);
            //ConsumeText.SetActive(false);
            Health[] dummies = (Health[])GameObject.FindObjectsOfType(typeof(Health));
            foreach (Health dummy in dummies)
            {
                if (dummy.AmIPlayer)
                {
                    dummy.RestoreLife();
                }
            }
            print("Consumed " + gameObject.name + " to restore life.");
        }

        Save();
    }
    public void OnEndHover()
    {
        if (!isInteractable && !isConsumable) return;
        if (isInteractable && !isConsumable)
        {
            print(gameObject.name + " OnEndHover");
        }
        else if (isInteractable && isConsumable)
        {
            //ConsumeText.SetActive(false);
            print(gameObject.name + " OnStartHover");
        }
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

    private void OnApplicationQuit()
    {
        Save();
    }

}
