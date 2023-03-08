using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    public bool IsInteractable => isInteractable;
    public bool isInteractable = true;
    public string savePath;

    public GameObject RelatedDoor;
    [SerializeField] float Speed = 5f;

    private void Awake()
    {
        Load();
        RelatedDoor = GameObject.FindGameObjectWithTag("Door");
        //remove outline of the object
        if (TryGetComponent<Outline>(out var outline))
        {
            outline.OutlineColor = new Color(outline.OutlineColor.r, outline.OutlineColor.g, outline.OutlineColor.b, 0);
        }
    }

    private void Start()
    {
        if (!isInteractable)
        {
            transform.eulerAngles = new Vector3
                (transform.eulerAngles.x + 100,
                transform.eulerAngles.y,
                transform.eulerAngles.z);

            if (TryGetComponent<Outline>(out var outline))
            {
                outline.OutlineColor = new Color(outline.OutlineColor.r, outline.OutlineColor.g, outline.OutlineColor.b, 0);
            }
        }
    }

    public void OnStartHover()
    {
        if (!isInteractable) return;
        //remove outline of the object
        if (TryGetComponent<Outline>(out var outline))
        {
            outline.OutlineColor = new Color(outline.OutlineColor.r, outline.OutlineColor.g, outline.OutlineColor.b, 1);
        }
    }

    public void OnEndHover()
    {
        if (!isInteractable) return;

        //remove outline of the object
        if (TryGetComponent<Outline>(out var outline))
        {
            outline.OutlineColor = new Color(outline.OutlineColor.r, outline.OutlineColor.g, outline.OutlineColor.b, 0);
        }
    }

    public void OnInteract()
    {
        if (isInteractable)
        { 
            transform.eulerAngles = new Vector3
                (transform.eulerAngles.x + 100,
                transform.eulerAngles.y,
                transform.eulerAngles.z);

            RelatedDoor.GetComponent<Door>().isLocked = false;
            isInteractable = false;

            if (TryGetComponent<Outline>(out var outline))
            {
                outline.OutlineColor = new Color(outline.OutlineColor.r, outline.OutlineColor.g, outline.OutlineColor.b, 0);
            }
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
        Save();
    }

}
