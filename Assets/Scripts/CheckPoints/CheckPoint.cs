using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    //private void Start()
    //{
    //    gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameMaster.instance.lastCheckPointPos = transform.position;
            Debug.Log("Entrato");
        }
    }
}
