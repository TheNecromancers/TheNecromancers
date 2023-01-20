using System.Collections;
using System.Collections.Generic;
using TheNecromancers.Combat;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] DialogueTrigger afterFirstAreaDialogue;
    Light[] playerLight;
    private void Awake()
    {
        if(player != null)
        {
            playerLight = player.GetComponentsInChildren<Light>();
        }
    }

    private void Start()
    {
        foreach(Light light in playerLight)
        {
           light.gameObject.SetActive(false);
        }
        
    }

    public void AfterFirstAreaInitialDialogue()
    {
        foreach (Light light in playerLight)
        {
            light.gameObject.SetActive(true);
        }
        afterFirstAreaDialogue.StartDialogue();
    }
}
