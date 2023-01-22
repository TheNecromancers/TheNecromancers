using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] float speed = 5f;

    private bool isInteractable = true;
    public bool IsInteractable => isInteractable;

    public bool isLocked = true;
    float rotationDegree;

    public string savePath;

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
    }

    private void Update()
    {
        if (!isLocked) Open();
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
        if (!isInteractable) return;
    }

    public void OnInteract()
    {
        if (!isInteractable) return;

        isLocked = false;
        isInteractable = false;
    }

    public void OnEndHover()
    {
        if (!isInteractable) return;
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
