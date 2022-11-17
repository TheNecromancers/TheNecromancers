using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] float speed = 5f;
    [SerializeField] bool isOpen = false; 
    bool shouldMove;

    void Open()
    {
        if (!isOpen)
            transform.rotation = Quaternion.Lerp(
             transform.rotation,
             Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z), Time.deltaTime * speed);
        else
            isOpen = true;
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
    }

    public void HitByRay()
    {
        Debug.Log("Message from raycaster!");
    }
}
