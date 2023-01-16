using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [field: SerializeField]public GameObject MainScreen;
    [field: SerializeField]public GameObject SettingsScreen;
    [field: SerializeField]public GameObject CreditsScreen;

    

    private void Awake() 
    {
            Controls controls = new Controls();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            controls.Player.Disable();
            controls.UIControls.Disable();    
            OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        MainScreen.SetActive(true);
        SettingsScreen.SetActive(false);
        CreditsScreen.SetActive(false);
    }

    public void OpenSettingsScreen()
    {
        MainScreen.SetActive(false);
        SettingsScreen.SetActive(true);
        CreditsScreen.SetActive(false);
    }
    public void OpenCreditsScreen()
    {
        MainScreen.SetActive(false);
        SettingsScreen.SetActive(false);
        CreditsScreen.SetActive(true);    
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
