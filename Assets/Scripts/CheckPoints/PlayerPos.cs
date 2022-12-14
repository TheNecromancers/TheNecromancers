using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{

    private void Awake()
    {
        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        //Debug.Log(gm.lastCheckPointPos + " Awake");
    }

    private void Start()
    {
        Debug.Log(GameMaster.instance.lastCheckPointPos + " Start");
        transform.position = GameMaster.instance.lastCheckPointPos;
    }
}
