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
    [field: SerializeField]public GameObject PauseScreenFirstSelected;
    [field: SerializeField]public GameObject SettingsScreenFirstSelected;
    [field: SerializeField]public Button ResumeGameButton;
    [field: SerializeField]public Button MainMenuButton;
    [field: SerializeField]public Button LoadGameButton;
    [field: SerializeField]public Button SettingsButton;
    [field: SerializeField]public Button ExitGameButton;
    [field: SerializeField]public Button BackButton;

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
    }
    private void OnDisable() 
    {
        inputManager.PauseMenuEvent -= OpenOptionMenu;
    }

    public void OnMainMenuLoad()
    {

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
        else
        {
            return;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MenuContainer.activeSelf)
            PreventDeselection();
        else 
        return;
    }
}
