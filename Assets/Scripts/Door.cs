using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] float speed = 5f;
    [SerializeField] bool CanInteract = true;
    [SerializeField] GameObject InteractiveText;

    bool shouldMove;

    private void Update()
    {
        if (shouldMove)
        {
            CanInteract = false;
            Open();
        }
    }

    void Open()
    {
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z), Time.deltaTime * speed);
    }

    public void Interact()
    {
        shouldMove = true;
    }

    public void InteractionDetected(bool value)
    {
        if (!CanInteract)
        {
            InteractiveText.SetActive(false);
            return;
        }

        InteractiveText.SetActive(value);
    }

    public bool IsInteractable()
    {
        return CanInteract;
    }
}
