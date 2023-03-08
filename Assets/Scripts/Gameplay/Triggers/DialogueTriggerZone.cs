using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TheNecromancers.StateMachine.Player;
using UnityEngine;

namespace TheNecromancers.StateMachine.Gameplay.Triggers
{
    public class DialogueTriggerZone : MonoBehaviour
    {
        public DialogueTrigger trigger;
        public string savePath;
        public bool isTriggered = false;

        private void Awake()
        {
            Load();
            trigger = GetComponent<DialogueTrigger>();

            if (isTriggered)
            {
                this.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerStateMachine Player))
            {
                trigger.StartDialogue();
                this.gameObject.SetActive(false);
                isTriggered = true;
                Save();
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
}
