using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject Light;
    [SerializeField] GameObject InteractiveText;

    private bool isInteractable = true;
    public bool IsInteractable => isInteractable;

    public void OnStartHover()
    {
        if (!isInteractable) return;

        InteractiveText.SetActive(true);
        print(gameObject.name + " OnStartHover");
    }
    public void OnInteract()
    {
        if (!isInteractable) return;
        isInteractable = false;

        Light.SetActive(true);
        InteractiveText.SetActive(false);
        print("Interact with" + gameObject.name);
    }
    public void OnEndHover()
    {
        if (!isInteractable) return;

        InteractiveText.SetActive(false);
        print(gameObject.name + " OnEndHover");
    }
}
