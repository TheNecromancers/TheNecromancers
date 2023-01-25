using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [field: SerializeField]public GameObject MainScreen;
    [field: SerializeField]public GameObject SettingsScreen;
    [field: SerializeField]public GameObject CreditsScreen;
    [field: SerializeField]public Button LoadGameButton;
    private Controls controls;
    private InputManager inputManager;

    

    private void Awake() 
    {

            inputManager =FindObjectOfType<InputManager>();
            LoadGameButton.onClick.AddListener(LoadMenu.Instance.OnClickBotton);
            LoadGameButton.onClick.AddListener(OnLoadGame);
            controls = new Controls();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            controls.Player.Disable();
            controls.UIControls.Disable();
            OpenMainMenu();
    }

    public void OnLoadGame()
    {
        if(inputManager != null)
        {
            inputManager.EnablePlayerControls();
            inputManager.EnableUIControls();
        }

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
    public void StartNewGame()
    {
        if(inputManager != null)
        {
            inputManager.EnablePlayerControls();
            inputManager.EnableUIControls();
        }

        SceneManager.LoadScene("Introduction");
    }

    void Start()
    {
            controls = new Controls();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            controls.Player.Disable();
            controls.UIControls.Disable();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
