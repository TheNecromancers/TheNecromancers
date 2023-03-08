using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaitingForExplorationGO : MonoBehaviour
{
    InputManager inputManager;
    [SerializeField] DialogueTrigger dialogue;
    public void ListenToExplorationAbility()
    {
        inputManager = FindObjectOfType<InputManager>();
        inputManager.ExplorationAbilityEvent += ExplorationListener;
    }

    public void ExplorationListener()
    {
        inputManager.ExplorationAbilityEvent -= ExplorationListener;
        dialogue.StartDialogue();

    }

    private void OnDestroy()
    {
        inputManager.ExplorationAbilityEvent -= ExplorationListener;
    }

    private void OnDisable()
    {
        inputManager.ExplorationAbilityEvent -= ExplorationListener;
    }
}
