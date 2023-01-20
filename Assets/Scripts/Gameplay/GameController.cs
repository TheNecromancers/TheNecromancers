using System.Collections;
using System.Collections.Generic;
using TheNecromancers.Combat;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] DialogueTrigger afterFirstAreaDialogue;
    Light playerLight;
    private void Awake()
    {
        if(player != null)
        {
            playerLight = player.GetComponentInChildren<Light>();
        }
    }

    private void Start()
    {
        playerLight.gameObject.SetActive(false);
    }

    public void AfterFirstAreaInitialDialogue()
    {
        playerLight.gameObject.SetActive(true);
        afterFirstAreaDialogue.StartDialogue();
    }
}
