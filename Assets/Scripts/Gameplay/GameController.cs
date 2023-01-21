using System.Collections;
using System.Collections.Generic;
using TheNecromancers.Combat;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] DialogueTrigger afterFirstAreaDialogue;
    Light[] playerLight;
    AbilityVisualController playerAVC;
    private void Awake()
    {
        if(player != null)
        {
            playerLight = player.GetComponentsInChildren<Light>();
            playerAVC = player.GetComponentInChildren<AbilityVisualController>();
        }
    }

    private void Start()
    {
        if (playerLight != null)
        {
            foreach (Light light in playerLight)
            {
                light.gameObject.SetActive(false);
            }
        }
        if(playerAVC != null)
        {
            playerAVC.ToggleOffImages();
        }
        
    }

    public void AfterFirstAreaInitialDialogue()
    {
        foreach (Light light in playerLight)
        {
            light.gameObject.SetActive(true);
        }
        playerAVC.ToggleOnImages();
        afterFirstAreaDialogue.StartDialogue();
    }

    public void AfterNarratorDialogue()
    {
        Loader.Load(Loader.Scene.First_Area);
        //Player.SetPlayerPosition(new Vector3(-16.5f, 1.09f, -22.88f));
    }
}
