using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{

    [SerializeField] ItemObject item;
    [SerializeField] GameObject InteractiveText;
    [SerializeField] bool CanInteract = true;
  
    public void Interact()
    {
        Player.instance.inventory.AddItem(item,1);
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
