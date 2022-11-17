using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] float speed = 5f;
    [SerializeField] bool isOpen = false;
    [SerializeField] GameObject InteractiveText;

    bool shouldMove;

    private void Update()
    {
        if (shouldMove)
        {
            Open();
        }
    }

    void Open()
    {
            transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z), Time.deltaTime * speed);
            isOpen = true;
    }

    public void Interact()
    {
        shouldMove = true;
    }

    public void InteractionDetected(bool value)
    {
        if (isOpen)
        {
            InteractiveText.SetActive(false);
            return;
        }

        InteractiveText.SetActive(value);
    }
}
