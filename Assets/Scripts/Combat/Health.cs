using UnityEngine;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Cci;

namespace TheNecromancers.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int MaxHealth = 100;

        [Header("Invulnerable Settings (Player Only)")]
        [Tooltip("Time be expressed in milliseconds")]
        [SerializeField] int TimeInvulnerableInMs;

        [Tooltip("Time be expressed in milliseconds")]
        [SerializeField] int LowHealthTimeInvulnerableInMs;

        [Tooltip("Percentage on MaxHealth")]
        [SerializeField] int HealthPercentage;

        [Header("Light Settings (Player Only)")]
        public bool AmIPlayer;
        //Da settare in codice Awake
        [SerializeField] HealthLightManager HealthLightManager;

        public int health;
        private bool isInvulnerable;

        public event Action OnTakeDamage;
        public event Action OnDie;

        public bool IsDead => health == 0;

        public string savePath;

        private void Start()
        {
            if (AmIPlayer)
            {
                Load();
                HealthLightManager = GetComponentInChildren<HealthLightManager>();
                HealthLightManager.ChangeLightAccordingToHealth(health, MaxHealth);
            }
            else
            {
                RestoreLife();
            }
        }

        public void SetInvulnerable(bool value)
        {
            isInvulnerable = value;
        }

        public void SetInvulnerable()
        {
            if(gameObject.CompareTag("Player"))
                HandleInvulnerable();
        }

        public void DealDamage(int damage)
        {
            if (health == 0) { return; }
            if (isInvulnerable) { return; }

            health = Mathf.Max(health - damage, 0);

            OnTakeDamage?.Invoke();
            if(AmIPlayer)
                HealthLightManager.ChangeLightAccordingToHealth(health, MaxHealth);
            if (health == 0)
            {
                OnDie?.Invoke();
            }

            Debug.Log(gameObject.name + " Current health " + health + " damage received " + damage);

            Save();
        }

        async void HandleInvulnerable()
        {
            if(health < (MaxHealth * HealthPercentage / 100))
            {
                TimeInvulnerableInMs = LowHealthTimeInvulnerableInMs;
            }

            isInvulnerable = true;
            await Task.Delay(TimeInvulnerableInMs);
            isInvulnerable = false;
        }

        public void RestoreLife()
        {
            health = MaxHealth;
            if (AmIPlayer)
                HealthLightManager.RestoreLifeColors();
            Save();
        }

        public void ChangePlayerIlluminationToDeath()
        {
            if (AmIPlayer)
                HealthLightManager.ChangePlayerIlluminationToDeath();
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