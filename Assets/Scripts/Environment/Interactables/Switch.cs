using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Rendering;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    public bool IsInteractable => isInteractable;
    public bool isInteractable = true;
    public string savePath;

    public GameObject RelatedDoor;
  
    public float rotationDegree;
    [SerializeField] float Speed = 5f; 

    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        // non funziona da fixare
        //switch (transform.localEulerAngles.x)
        //{
        //    case -50:
        //        rotationDegree = 100f; 
        //        break;

        //    case 50:
        //        rotationDegree = -100f;
        //        break;

        //    default:
        //        break;
        //}
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
    }

    public void OnInteract()
    {
        if (isInteractable)
        {
            // Da fixare non ruota help!
            //transform.localRotation = Quaternion.Lerp(transform.localRotation,
            //    Quaternion.Euler(rotationDegree,
            //    transform.localRotation.y, transform.localRotation.z),
            //    Time.deltaTime * Speed);

            RelatedDoor.GetComponent<Door>().isLocked = false;
            isInteractable = false;
        }
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
