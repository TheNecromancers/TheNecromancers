using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{


    [field: SerializeField]public GameObject MenuContainer;
    [field: SerializeField]public GameObject MainScreen;
    [field: SerializeField]public GameObject SettingsScreen;
    [field: SerializeField]public GameObject PCControlSchemeScreen;
    [field: SerializeField]public GameObject GamepadControlSchemeScreen;
    [field: SerializeField]public GameObject PauseScreenFirstSelected;
    [field: SerializeField]public GameObject SettingsScreenFirstSelected;
    [field: SerializeField]public Button ResumeGameButton;
    [field: SerializeField]public Button MainMenuButton;
    [field: SerializeField]public Button LoadGameButton;
    [field: SerializeField]public Button SettingsButton;
    [field: SerializeField]public Button ExitGameButton;
    [field: SerializeField]public Button BackButton;
    [field: SerializeField]public Button ControlsButton;
    [field: SerializeField]public Button GamepadButton;
    [field: SerializeField]public Button PCSChemeButton;


    //private Controls controls;
    private InputManager inputManager;

    private void OnEnable() 
    {
        inputManager = FindObjectOfType<InputManager>();
        inputManager.PauseMenuEvent += OpenOptionMenu;
        SettingsButton.onClick.AddListener(ShowSettingsMenu);
        ResumeGameButton.onClick.AddListener(OpenOptionMenu);
        BackButton.onClick.AddListener(ShowPauseScreen);
        MainMenuButton.onClick.AddListener(GoToMainMenu);
        MainMenuButton.onClick.AddListener(OnMainMenuLoad);
        ExitGameButton.onClick.AddListener(QuitGame);
        LoadGameButton.onClick.AddListener(LoadMenu.Instance.OnClickBotton);
        ControlsButton.onClick.AddListener(()=> ShowTutorialScreen(GamepadControlSchemeScreen,GamepadButton));
        GamepadButton.onClick.AddListener(()=>ShowTutorialScreen(PCControlSchemeScreen,PCSChemeButton));
        PCSChemeButton.onClick.AddListener(ShowSettingsMenu);
        PCSChemeButton.onClick.AddListener(DisableTutorialScreen);
        DisableTutorialScreen();
    }
    private void OnDisable() 
    {
        inputManager.PauseMenuEvent -= OpenOptionMenu;
    }

    public void OnMainMenuLoad()
    {
        if (PlayerInstance.Instance.gameObject is not null)
        {
            Destroy(PlayerInstance.Instance.gameObject);
        }
            inputManager.DisablePlayerControls();
            inputManager.DisableUIControls();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            MenuContainer.SetActive(false);
    }

    public void OpenOptionMenu()
    {

        if(!MenuContainer.activeSelf)
            
        {

            MenuContainer.SetActive(true);
            ShowPauseScreen();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            inputManager.PauseMenuStateChange();
        }
        else
        {
            MenuContainer.SetActive(false);
            MainScreen.SetActive(false);
            SettingsScreen.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            inputManager.PauseMenuStateChange();
        }
    }

    public void ShowPauseScreen()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PauseScreenFirstSelected);
        MainScreen.SetActive(true);
        SettingsScreen.SetActive(false);
    }

    public void ShowSettingsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(SettingsScreenFirstSelected);
        MainScreen.SetActive(false);
        SettingsScreen.SetActive(true);
    }

    public void DisableTutorialScreen()
    {
        GamepadControlSchemeScreen.SetActive(false);
        PCControlSchemeScreen.SetActive(false);
    }
    public void ShowTutorialScreen(GameObject screen, Button forcedButtonSelection)
    {
        DisableTutorialScreen();
        screen.SetActive(true);
        PreventDeselectionForced(forcedButtonSelection.gameObject);
        
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }


    public void PreventDeselectionForced(GameObject sel)
    {
        if(EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject != sel)
            {
                EventSystem.current.SetSelectedGameObject(sel);
            }
    }
    public void PreventDeselection()
    {
        GameObject sel;
        if(MainScreen.activeSelf)
        {
            sel = PauseScreenFirstSelected;
            if(EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject != sel)
            {
                sel = EventSystem.current.currentSelectedGameObject;
            }
            else if(sel != null && EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(sel);
            }
        }
        else if(SettingsScreen.activeSelf)
        {
            sel = SettingsScreenFirstSelected;
            if(EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject != sel)
            {
                sel = EventSystem.current.currentSelectedGameObject;
            }
            else if(sel != null && EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(sel);
            }
        }
        else if(GamepadControlSchemeScreen.activeSelf)
        {
            PreventDeselectionForced(GamepadButton.gameObject);
        }
        else if(PCControlSchemeScreen.activeSelf)
        {
            PreventDeselectionForced(PCSChemeButton.gameObject);
        }
        else
        {
            return;
        }

    }

    void Update()
    {
        if(MenuContainer.activeSelf)
            PreventDeselection();
        else 
        return;
    }
}
