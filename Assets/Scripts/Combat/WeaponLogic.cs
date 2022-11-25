using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    [SerializeField] LayerMask LayerMaskToInteract;
    private int damage;
    private float knockback;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("First enter " + other.name);
        if (!((LayerMaskToInteract.value & (1 << other.transform.gameObject.layer)) > 0)) { return; }

        Debug.Log("Second enter " + other.name);

        if (alreadyCollidedWith.Contains(other)) { return; }

        Debug.Log("Third enter " + other.name);

        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            Debug.Log("Deal Damage " + other.name);

            health.DealDamage(damage);
        }

        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Debug.Log("Apply force " + other.name);

            Vector3 direction = (other.transform.position - transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
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
}
