using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] float speed = 5f;
    bool shouldMove;
    void Open()
    {
            transform.rotation = Quaternion.Lerp(
             transform.rotation,
             Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z), Time.deltaTime * speed);
    }
    private void Update()
    {
        if(shouldMove)
        {
            Open();
        }
    }
    public void Interact()
    {
        shouldMove = true;
        Debug.Log("Sto interaggendo con " + gameObject.name);
    }

  
}
