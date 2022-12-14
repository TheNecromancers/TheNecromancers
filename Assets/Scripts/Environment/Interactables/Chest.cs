using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheNecromancers.StateMachine.Player;

public class Chest : MonoBehaviour, IInteractable
{


    private bool isInteractable = true;
    public bool IsInteractable => isInteractable;
    [SerializeField] ItemObject item;
    [SerializeField] GameObject InteractiveText;

    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
  
    public void OnInteract()
    {
        if (!isInteractable) return;

        AddItemToInventory(player.GetComponent<PlayerStateMachine>().inventoryObject);
        InteractiveText.SetActive(false);
        print("Interact with " + gameObject.name);
        isInteractable =false;
    }

        public void OnStartHover()
    {
        if (!isInteractable) return;

        InteractiveText.SetActive(true);
        print(gameObject.name + " OnStartHover");
    }


    public void OnEndHover()
    {
        if (!isInteractable) return;

        InteractiveText.SetActive(false);
        print(gameObject.name + " OnEndHover");
    }

    void AddItemToInventory(InventoryObject inventory)
    {
        inventory.AddItem(item, 1);
    }


}
