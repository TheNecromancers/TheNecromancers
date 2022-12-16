using System.Collections;
using System.Collections.Generic;
using TheNecromancers.Combat;
using UnityEngine;

public class Torch : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject Light;
    [SerializeField] GameObject ConsumeText;

    private bool isConsumable = false;
    private bool isInteractable = true;
    public bool IsInteractable => isInteractable;

    public void OnStartHover()
    {
        if (!isInteractable && !isConsumable) return;
        if (isInteractable)
        {
            print(gameObject.name + " OnStartHover");
        }
        else if (isConsumable)
        {
            ConsumeText.SetActive(true);
            print(gameObject.name + " OnStartHover");
        }
    }
    public void OnInteract()
    {
        if (!isInteractable && !isConsumable) return;
        if (isInteractable)
        {
            isInteractable = false;
            isConsumable = true;
            Light.SetActive(true);
            print("Interact with" + gameObject.name);
        }
        else if (isConsumable)
        {
            isConsumable = false;
            Light.SetActive(false);
            ConsumeText.SetActive(false);
            Health[] dummies = (Health[])GameObject.FindObjectsOfType(typeof(Health));
            foreach (Health dummy in dummies)
            {
                if (dummy.AmIPlayer)
                {
                    dummy.RestoreLife();
                }
            }
            print("Consumed " + gameObject.name + " to restore life.");
        }
    }
    public void OnEndHover()
    {
        if (!isInteractable && !isConsumable) return;
        if (isInteractable)
        {
            print(gameObject.name + " OnEndHover");
        }
        else if (isConsumable)
        {
            ConsumeText.SetActive(false);
            print(gameObject.name + " OnStartHover");
        }
    }
}
