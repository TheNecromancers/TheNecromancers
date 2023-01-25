using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoSingleton<LoadMenu>
{
    public string CurrentNameScene;
    public Scene CurrentScene;
    public string savePath;

    private void Start()
    {
        Load();
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    public void ChangedActiveScene(Scene current, Scene next)
    {
        Debug.Log(next.name);
        if (current.name != "Loading")
        {
            CurrentNameScene = SceneManager.GetActiveScene().name;
            CurrentScene = SceneManager.GetActiveScene();
            //CurrentNameScene = current.name;
            //CurrentScene = current;
        }
        Save();
    }

    private void OnClickBotton()
    {
        SceneManager.LoadScene(CurrentNameScene);
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
        CurrentScene = SceneManager.GetActiveScene();
        CurrentNameScene = CurrentScene.name;
        Save();
    }
}
