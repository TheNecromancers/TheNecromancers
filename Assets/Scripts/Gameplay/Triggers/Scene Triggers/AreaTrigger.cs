using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;

public abstract class AreaTrigger : MonoBehaviour
{
    public Vector3 PlayerPos;

    public abstract void OnTriggerEnter(Collider other);

}
