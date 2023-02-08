using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{


    [field: SerializeField]public GameObject MenuContainer;
    [field: SerializeField]public GameObject MainScreen;
    [field: SerializeField]public GameObject SettingsScreen;
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
        }
        else
        {
            MenuContainer.SetActive(false);
            MainScreen.SetActive(false);
            SettingsScreen.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
    }

    public void ShowPauseScreen()
    {
        MainScreen.SetActive(true);
        SettingsScreen.SetActive(false);
    }

    public void ShowSettingsMenu()
    {
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
