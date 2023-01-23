using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheNecromancers.StateMachine.Player;

public class PickuppableItem : MonoBehaviour, IInteractable
{
    public bool isInteractable = true;
    public bool IsInteractable => isInteractable;
    [SerializeField] ItemObject item;

    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnInteract()
    {
        if (!isInteractable) return;

        AddItemToInventory(player.GetComponent<PlayerStateMachine>().inventoryObject);
        print("Interact with " + gameObject.name);

        isInteractable = false;
        gameObject.SetActive(false);
    }

    public void OnStartHover()
    {
        if (!isInteractable) return;

        print(gameObject.name + " OnStartHover");

    }

    public void OnEndHover()
    {
        if (!isInteractable) return;

        print(gameObject.name + " OnEndHover");

    }

    void AddItemToInventory(InventoryObject inventory)
    {
        inventory.AddItem(item, 1);
    }
}
