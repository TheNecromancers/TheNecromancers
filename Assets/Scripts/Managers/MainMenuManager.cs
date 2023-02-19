using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [field: SerializeField]public GameObject MainScreen;
    [field: SerializeField]public GameObject SettingsScreen;
    [field: SerializeField]public GameObject CreditsScreen;
    [field: SerializeField]public GameObject MainScreenFirstSelected;
    [field: SerializeField]public GameObject SettingsScreenFirstSelected;
    [field: SerializeField]public GameObject CreditsScreenFirstSelected;
    [field: SerializeField]public Button LoadGameButton;
    [field: SerializeField]public InventoryObject Inventory;

    private Controls controls;
    private InputManager inputManager;

    

    private void Awake() 
    {
            Inventory = Resources.Load<InventoryObject>("Empty Inventory");
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
        Debug.Log("trovato player"+ PlayerInstance.Instance);
        if (PlayerInstance.Instance != null)
        {
            Debug.Log("dentro al check");
            Destroy(PlayerInstance.Instance.gameObject);
        }
        if(inputManager != null)
        {
            inputManager.EnablePlayerControls();
            inputManager.EnableUIControls();
        }

    }

    public void OpenMainMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(MainScreenFirstSelected);
        MainScreen.SetActive(true);
        SettingsScreen.SetActive(false);
        CreditsScreen.SetActive(false);
    }

    public void OpenSettingsScreen()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(SettingsScreenFirstSelected);
        MainScreen.SetActive(false);
        SettingsScreen.SetActive(true);
        CreditsScreen.SetActive(false);
    }
    public void OpenCreditsScreen()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(CreditsScreenFirstSelected);
        MainScreen.SetActive(false);
        SettingsScreen.SetActive(false);
        CreditsScreen.SetActive(true);    
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                         Application.Quit();
        #endif
    }
    public void StartNewGame()
    {
        if (PlayerInstance.Instance != null)
        {
            Destroy(PlayerInstance.Instance.gameObject);
        }
        if (inputManager != null)
        {
            inputManager.EnablePlayerControls();
            inputManager.EnableUIControls();
        }

        if (Inventory != null)
        {
            Inventory.ClearInventory();
        }
        //TODO: Fix delete of files, because it crashes from app but not from unity
        if (Directory.Exists(Path.Combine(Application.persistentDataPath, "Data")))
        {
            string[] filePaths = Directory.GetFiles(string.Concat(Application.persistentDataPath, "/Data"));
            foreach (string filePath in filePaths) File.Delete(filePath);
        }
        SceneManager.LoadScene("TutorialCommands");
        
    }

    public void PreventDeselection()
    {
        GameObject sel;
        if(MainScreen.activeSelf)
        {
            sel = MainScreenFirstSelected;
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
        else if(CreditsScreen.activeSelf)
        {
            sel = CreditsScreenFirstSelected;
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
        PreventDeselection();
    }
}
