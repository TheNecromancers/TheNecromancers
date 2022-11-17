using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject Light;
    [SerializeField] GameObject InteractiveText;
    bool isOn;
  
    public void Interact()
    {
        Light.SetActive(true);
        isOn = true;
    }

    public void InteractionDetected(bool value)
    {
        if (isOn)
        {
            InteractiveText.SetActive(false);
            return;
        }

        InteractiveText.SetActive(value);
    }
}
