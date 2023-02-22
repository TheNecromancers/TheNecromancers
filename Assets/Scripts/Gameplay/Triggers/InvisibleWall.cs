using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    public string savePath;
    public bool isDisable = false;

    private void Start()
    {
        Load();
        if (isDisable)
        {
            DisableTheWall();
        }
    }

    public void DisableTheWall()
    {
        this.gameObject.SetActive(false);
        isDisable = true;
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

}
