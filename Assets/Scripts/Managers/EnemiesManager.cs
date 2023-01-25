using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TheNecromancers.StateMachine.Enemy;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public EnemyStateMachine[] Enemies;

    public int nonActive;

    public string savePath;

    private void Start()
    {
        Load();

        Enemies = FindObjectsOfType<EnemyStateMachine>(true);

        if (Enemies.Length == nonActive)
        {
            Reset();
            nonActive = 0;
        }
        else
        {
            nonActive = 0;
        }
    }

    private void Reset()
    {
        for(int x = 0; x < Enemies.Length; x++)
        {
            Enemies[x].gameObject.SetActive(false);
        }
    }

    public void EnemiesDead()
    {
        for (int i = 0; i < Enemies.Length; i++)
        {
            Debug.Log(Enemies[i].gameObject.activeSelf + " active");
            Debug.Log(Enemies[i].gameObject.activeInHierarchy + " in hierarchy");

            if (Enemies[i].gameObject.activeSelf == false)
            {
                nonActive += 1;
            }
        }

        Save();
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
