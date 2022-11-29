using System.Collections.Generic;
using UnityEngine;

namespace TheNecromancers.Combat
{
    public class WeaponLogic : MonoBehaviour
    {
        [SerializeField] LayerMask LayerToInteract;
        private int damage;
        private float knockback;

        private List<Collider> alreadyCollidedWith = new List<Collider>();

        private void OnEnable()
        {
            alreadyCollidedWith.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!((LayerToInteract.value & (1 << other.transform.gameObject.layer)) > 0)) { return; }

            if (alreadyCollidedWith.Contains(other)) { return; }

            alreadyCollidedWith.Add(other);

            if (other.TryGetComponent<Health>(out Health health))
            {
                health.DealDamage(damage);
            }

            if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
            {
                Vector3 direction = (other.transform.position - transform.position).normalized;
                direction.y = 0;
                forceReceiver.AddForce(direction * knockback);
                return;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            alreadyCollidedWith.Clear();
        }

        public void SetAttack(int damage, float knockback)
        {
            this.damage = damage;
            this.knockback = knockback;
        }

        public void SetAttack(int damage, float knockback, bool isEnemy)
        {
            this.damage = damage;
            this.knockback = knockback;

            if(isEnemy)
            {
                LayerToInteract.value = 1 << LayerMask.NameToLayer("Player");
            }
            else
            {
                LayerToInteract.value = 1 << LayerMask.NameToLayer("Enemy");
            }
        }
    }
}