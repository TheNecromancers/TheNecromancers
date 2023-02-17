using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TheNecromancers.Combat;
using UnityEngine;

public class Torch : MonoBehaviour, IInteractable
{
    [SerializeField] protected Light Light;
    [SerializeField] GameObject ConsumeText;

    public bool isConsumable = false;
    public bool isInteractable = true;
    public bool IsInteractable => isInteractable;
    public string savePath;

    private void Awake()
    {
        Load();

        Light = GetComponentInChildren(typeof(Light),true) as Light;

        if ((!isInteractable && isConsumable) ^ (isInteractable && isConsumable))
        {
            Light.gameObject.SetActive(true);
        }
    }

    public void OnStartHover()
    {
        if (!isInteractable && !isConsumable) return;
        //remove outline of the object
        Outline outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.OutlineColor = new Color(outline.OutlineColor.r, outline.OutlineColor.g, outline.OutlineColor.b, 1);
        }
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
            Light.gameObject.SetActive(true);
            print("Interact with" + gameObject.name);
        }
        else if (isInteractable && isConsumable)
        {
            bool isPlayerMaxHealth = false;
            Health[] dummies = (Health[])GameObject.FindObjectsOfType(typeof(Health));
            foreach (Health dummy in dummies)
            {
                if (dummy.AmIPlayer)
                {
                    if (dummy.AmIMaxHealth())
                    {
                        isPlayerMaxHealth = true;
                    }
                    else
                    {
                        dummy.RestoreLife();
                    }
                    
                }
            }
            if (!isPlayerMaxHealth)
            {
                isInteractable = false;
                isConsumable = false;
                Light.gameObject.SetActive(false);
                //remove outline of the object
                Outline outline = GetComponent<Outline>();
                if (outline != null)
                {
                    outline.OutlineColor = new Color(outline.OutlineColor.r, outline.OutlineColor.g, outline.OutlineColor.b, 0);
                }
                //ConsumeText.SetActive(false);
                print("Consumed " + gameObject.name + " to restore life.");
            }
        }

        Save();
    }
    public void OnEndHover()
    {
        if (!isInteractable && !isConsumable) return;
        //remove outline of the object
        Outline outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.OutlineColor = new Color(outline.OutlineColor.r, outline.OutlineColor.g, outline.OutlineColor.b, 0);
        }
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
