using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LastCheckPointPos = transform.position;
            Debug.Log("Entrato");
        }
    }
}
