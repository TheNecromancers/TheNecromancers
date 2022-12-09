using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameMaster gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gm.lastCheckPointPos = transform.position;
            Debug.Log("Entrato");
        }
    }
}
