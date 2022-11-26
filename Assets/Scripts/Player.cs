using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public static Player instance;
    private void Awake() 
    {
        if (instance != null && instance != this) 
    { 
        Destroy(this); 
    } 
    else 
    { 
        instance = this; 
    } 
    }

    private void OnApplicationQuit() 
    {
        inventory.Container.Clear();
    }


}
