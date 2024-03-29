using System.Collections;
using System.Collections.Generic;
using TheNecromancers.Combat;
using TheNecromancers.StateMachine.Gameplay.Triggers;
using UnityEngine;
using TheNecromancers.StateMachine.Player;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] DialogueTrigger afterFirstAreaDialogue;
    Light[] playerLight;
    AbilityVisualController playerAVC;
    public PlayerStateMachine playerStateMachine;
    private void Awake()
    {
        //player = FindObjectOfType<PlayerStateMachine>().gameObject;

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

        DialogueTriggerZone Dz = FindObjectOfType<DialogueTriggerZone>();

        if (Dz == null && playerLight!=null)
        {
            foreach (Light light in playerLight)
            {
                light.gameObject.SetActive(true);
            }
            playerAVC.ToggleOnImages();
        }
    }

    public void AfterFirstAreaInitialDialogue()
    {
        foreach (Light light in playerLight)
        {
            if (light != null)
            {
                //Change light gradually as requested by designers
                light.intensity = 0;
                light.range = 0;
                light.gameObject.SetActive(true);
                HealthLightManager hlm = light.GetComponent<HealthLightManager>();
                if (hlm != null) hlm.RestoreLifeColors();
            }
        }

        playerAVC.ToggleOnImages();
        afterFirstAreaDialogue.StartDialogue();
    }

    public void AfterNarratorDialogue()
    {
        Loader.Load(Loader.Scene.First_Area);

        playerStateMachine = FindObjectOfType<PlayerStateMachine>();

        if(playerStateMachine != null)
        {
            playerStateMachine.SetPlayerPosition(new Vector3(-16.5f, 5f, -24f));           
        }

    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
