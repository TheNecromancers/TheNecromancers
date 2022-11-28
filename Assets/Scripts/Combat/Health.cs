using UnityEngine;
using System;
using System.Collections;

namespace TheNecromancers.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHealth = 100;

        [Header("Invulnerable Settings (Only On Player)")]
        [SerializeField] float TimeInvulnerable;
        [SerializeField] float LowHealthTimeInvulnerable;
        [Tooltip("Percentage on MaxHealth")]
        [SerializeField] int HealthPercentage;

        private int health;
        private bool isInvulnerable;

        public event Action OnTakeDamage;
        public event Action OnDie;

        public bool IsDead => health == 0;

        private void Start()
        {
            health = maxHealth;
        }

        public void SetInvulnerable(bool value)
        {
            isInvulnerable = value;
        }

        public void SetInvulnerable()
        {
            if(gameObject.CompareTag("Player"))
                StartCoroutine(HandleInvulnerable());
        }

        public void DealDamage(int damage)
        {
            if (health == 0) { return; }
            if (isInvulnerable) { return; }

            health = Mathf.Max(health - damage, 0);

            OnTakeDamage?.Invoke();

            if (health == 0)
            {
                OnDie?.Invoke();
            }

            Debug.Log("Current health " + health + " damage received " + damage);
        }

        IEnumerator HandleInvulnerable()
        {
            if(health < (maxHealth * HealthPercentage / 100))
            {
                TimeInvulnerable = LowHealthTimeInvulnerable;
            }

            isInvulnerable = true;
            yield return new WaitForSeconds(TimeInvulnerable);
            isInvulnerable = false;
        }
    }
}