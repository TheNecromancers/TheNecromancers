using UnityEngine;
using System;
using System.Threading.Tasks;

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
        [SerializeField] HealthLightManager HealthLightManager;

        private int health;
        private bool isInvulnerable;

        public event Action OnTakeDamage;
        public event Action OnDie;

        public bool IsDead => health == 0;

        private void Start()
        {
            //if (AmIPlayer == false)
            //{
            //    RestoreLife();
            //}

            RestoreLife();
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
        }

        public void ChangePlayerIlluminationToDeath()
        {
            if (AmIPlayer)
                HealthLightManager.ChangePlayerIlluminationToDeath();
        }
    }
}