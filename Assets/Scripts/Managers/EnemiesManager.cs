using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TheNecromancers.StateMachine.Enemy;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public EnemyStateMachine[] Enemies;
    public GameObject[] Triggers;
    public GameObject[] InvisibleWalls;

    public bool IsCompleted;

    public string savePath;

    public int enemies;

    private void Start()
    {
        Load();

        Enemies = FindObjectsOfType<EnemyStateMachine>(true);
        Triggers = GameObject.FindGameObjectsWithTag("Trigger");
        InvisibleWalls = GameObject.FindGameObjectsWithTag("InvisibleWall");

        if (IsCompleted)
        {
            ActiveTriggers();
            Reset();
        }
        else if (!IsCompleted)
        {
            DisableTrigger();
        }
    }

    private void Reset()
    {
        for(int x = 0; x < Enemies.Length; x++)
        {
            Enemies[x].gameObject.SetActive(false);
        }
    }

    public void ActiveTriggers()
    {
        for (int y = 0; y < Triggers.Length; y++)
        {
            Triggers[y].SetActive(true);
        }

        if (InvisibleWalls.Length > 0)
        {
            for (int w = 0; w < InvisibleWalls.Length; w++)
            {
                InvisibleWalls[w].SetActive(false);
            }
        }
      
    }

    public void DisableTrigger()
    {
        for (int z = 0; z < Triggers.Length; z++)
        {
            Triggers[z].SetActive(false);
        }

        if (InvisibleWalls.Length> 0)
        {
            for (int q = 0; q < InvisibleWalls.Length; q++)
            {
                InvisibleWalls[q].SetActive(true);
            }
        }

    }

    private void Update()
    {
        if (!IsCompleted)
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i].gameObject.activeSelf == false)
                {
                    enemies += 1;
                }
            }

            if (Enemies.Length == enemies)
            {
                ActiveTriggers();
                IsCompleted = true;
                Save();
            }

            enemies = 0;
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
