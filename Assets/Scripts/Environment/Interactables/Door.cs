using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TheNecromancers.StateMachine.Player;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] float speed = 5f;

    private bool isInteractable = true;
    public bool IsInteractable => isInteractable;

    public bool IsASwitchDoor = false;
    public bool IsAKeyDoor = false;
    public bool isLocked = true;
    float rotationDegree;

    public string savePath;
    public GameObject RelatedKey = null;

    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        Debug.Log(transform.eulerAngles.y);
        switch (transform.eulerAngles.y)
        {
            case -90:
                rotationDegree = -120f;
                break;
            case 90f:
                rotationDegree = 20f;
                break;
            case 0:
                rotationDegree = 160f;
                break;
            case -180:
                rotationDegree = 160f;
                break;
            default:
                break;
        }
        
        if (IsAKeyDoor)
        {
            RelatedKey = GameObject.FindGameObjectWithTag("Key");
            if (RelatedKey != null)
            {
                isInteractable = false;
            }
        }
    }

    private void Update()
    {
        if (!isLocked) Open();

        if (IsAKeyDoor)
        {
            if (!RelatedKey.gameObject.activeSelf) 
            {
                isInteractable = true;
            }
        }
    }
  

    void Open()
    {
        transform.localRotation = Quaternion.Lerp(
        transform.localRotation,
        Quaternion.Euler(transform.localRotation.x,
        rotationDegree, 
        transform.localRotation.z), 
        Time.deltaTime * speed);
        Save();
    }

    public void OnStartHover()
    {
        if (!isInteractable ^ IsASwitchDoor) return;
        //remove outline of the object
        Outline outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.OutlineColor = new Color(outline.OutlineColor.r, outline.OutlineColor.g, outline.OutlineColor.b, 1);
        }
    }

    public void OnInteract()
    {
        if (!isInteractable ^ IsASwitchDoor) return;

        isLocked = false;
        isInteractable = false;
    }

    public void OnEndHover()
    {
        if (!isInteractable ^ IsASwitchDoor) return;
        //remove outline of the object
        Outline outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.OutlineColor = new Color(outline.OutlineColor.r, outline.OutlineColor.g, outline.OutlineColor.b, 0);
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
