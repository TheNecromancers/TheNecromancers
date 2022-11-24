using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] float speed = 5f;
    [SerializeField] GameObject InteractiveText;

    private bool isInteractable = true;
    public bool IsInteractable => isInteractable;

    bool shouldOpen = false;

    private void Update()
    {
       if (shouldOpen)Open();
    }

    void Open()
    {
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z), Time.deltaTime * speed);
    }

    public void OnStartHover()
    {
        if (!isInteractable) return;

        InteractiveText.SetActive(true);
        print("OnStartHover");
    }

    public void OnInteract()
    {
        if (!isInteractable) return;

        shouldOpen = true;
        isInteractable = false;
        InteractiveText.SetActive(false);
        print("OnInteract " + gameObject.name);
    }

    public void OnEndHover()
    {
        if (!isInteractable) return;

        InteractiveText.SetActive(false);
        print("OnEndHover");
    }
}
