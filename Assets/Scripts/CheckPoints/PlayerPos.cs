using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    private void Start()
    {
        transform.position = GameManager.Instance.LastCheckPointPos;
    }
}
