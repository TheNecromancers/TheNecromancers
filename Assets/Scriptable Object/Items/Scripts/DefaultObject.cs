using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Default Object", menuName ="Inventory System/Items/Default")]
public class DefaultObject : ItemObject
{

    public void Awake()
    {
        type = ItemType.Default;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
