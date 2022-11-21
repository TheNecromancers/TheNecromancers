using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    void Interact();

    bool IsInteractable();

    void InteractionDetected(bool Detected);
}

