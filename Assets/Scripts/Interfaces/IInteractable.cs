using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    bool IsInteractable { get; }

    void OnStartHover();
    void OnInteract();
    void OnEndHover();

}

