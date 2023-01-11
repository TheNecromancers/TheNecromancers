using System.Collections;
using System.Collections.Generic;
using TheNecromancers.Combat;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int Speed;
    [SerializeField] int Damage;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * Speed;
    }

    void OnEnable()
    {
        StartCoroutine(DisableObject(5));
    }

    IEnumerator DisableObject(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Health health))
            {
                health.DealDamage(Damage);
                Destroy(gameObject);
            }
        }
    }
}