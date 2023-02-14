using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class TutorialInfoSaver : MonoBehaviour
{
    public string savePath;
    public bool isTriggered = false;

    private void Awake()
    {
        Load();

    }

    public void PanelOpened()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        InputManager playerInput = FindObjectOfType<InputManager>();
        if (playerInput != null)
        {
            playerInput.DisablePlayerControls();
            playerInput.DisableUIControls();
        }
    }
    public void PanelTriggered()
    {
        InputManager playerInput = FindObjectOfType<InputManager>();
        if (playerInput != null)
        {
            playerInput.EnablePlayerControls();
            playerInput.EnableUIControls();
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isTriggered = true;
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
