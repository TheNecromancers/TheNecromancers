using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject Light;
    [SerializeField] GameObject InteractiveText;
    [SerializeField] bool CanInteract = true;
  
    public void Interact()
    {
        Light.SetActive(true);
        CanInteract = false;
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
