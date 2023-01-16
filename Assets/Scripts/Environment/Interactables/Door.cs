using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] float speed = 5f;

    private bool isInteractable = true;
    public bool IsInteractable => isInteractable;

    public bool isLocked = true;
    float rotationDegree;

    private void Update()
    {
        if (!isLocked) Open();
    }

    private void Start()
    {
        if (transform.eulerAngles.y == 180f)
        {
            rotationDegree = 20f;
        }
        else
        {
            rotationDegree = 160f;
        }
    }

    void Open()
    {
        transform.localRotation = Quaternion.Lerp(
        transform.localRotation,
        Quaternion.Euler(transform.localRotation.x,
        rotationDegree, 
        transform.localRotation.z), 
        Time.deltaTime * speed); 
    

    }

    public void OnStartHover()
    {
        if (!isInteractable) return;
    }

    public void OnInteract()
    {
        if (!isInteractable) return;

        isLocked = false;
        isInteractable = false;

        print("Interact with " + gameObject.name);
    }

    public void OnEndHover()
    {
        if (!isInteractable) return;
    }
}
