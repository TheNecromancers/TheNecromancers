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
        if (next.name != "MainMenu")
        {
            CurrentNameScene = SceneManager.GetActiveScene().name;
            CurrentScene = SceneManager.GetActiveScene();
        }
        Save();
    }

    public void OnClickBotton()
    {
        Time.timeScale =1;
        SceneManager.LoadScene(CurrentNameScene);
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
        if(SceneManager.GetActiveScene().name != "MainMenu")
        {
            CurrentScene = SceneManager.GetActiveScene();
            CurrentNameScene = CurrentScene.name;
        }

        Save();
    }
}
