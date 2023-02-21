using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;
    public Image actorImage;
    public TMP_Text actorText;
    public TMP_Text messageText;
    public RectTransform backgroundBox;
    public Button nextButton;
    public Button skipButton;
    private bool isDisplayingMessage = false;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    //bool isActive = false;
    UnityEvent EndDialogueEvent;

    public static DialogueManager Instance
    {
        get
        {
            if(_instance is null)
            {
                Debug.LogError("Dialogue Manager is NULL");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        isDisplayingMessage = false;
    }
    public void OpenDialogue(Message[] messages, Actor[] actors, UnityEvent OnEndDialogue)
    {
        EndDialogueEvent = OnEndDialogue;
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        //isActive = true;
        nextButton.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(true);
        Debug.Log("Started conversation! Loaded messages: "+ messages.Length);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        InputManager playerInput = FindObjectOfType<InputManager>();
        if(playerInput != null)
        {
            playerInput.DisablePlayerControls();
            playerInput.DisableUIControls();
        }
        DisplayMessage();
        //Animation duration and Scale of the box for the dialogue
        backgroundBox.LeanScale(Vector3.one, 0.5f).setEaseInOutExpo();
    }

    void DisplayMessage()
    {
        isDisplayingMessage = !isDisplayingMessage;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(nextButton.gameObject);
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;
        //Animation to fadein the text
        FadeInTextColor(messageText,0.5f);
         //End Animation
         Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorText.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
        //Animation to fadein the buttons
        FadeInButtonColor(nextButton, 0.5f);
        FadeInButtonColor(skipButton, 0.5f);
    }

    void FadeInButtonColor(Button btn, float duration)
    {
        var btnColor = btn.image.color;
        btnColor.a = 1;
        var fadeoutBtnColor = btnColor;
        fadeoutBtnColor.a = 0;
        LeanTween.value(skipButton.gameObject, a => btn.image.color = a, fadeoutBtnColor, btnColor, duration);
    }

    void FadeInTextColor(TMP_Text txt, float duration)
    {
        var color = messageText.color;
        color.a = 1;
        var fadeoutColor = color;
        fadeoutColor.a = 0;
        LeanTween.value(txt.gameObject, a => txt.color = a, fadeoutColor, color, 0.5f);
    }


    public void NextMessage()
    {
        Debug.Log("Next Message");
        activeMessage++;
        if(activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Conversation Ended!");
            EndConversation();
        }
    }

    public void SkipMessages()
    {
        Debug.Log("Skip Message");
        activeMessage = currentMessages.Length;
        EndConversation();
    }

    private void EndConversation()
    {
        isDisplayingMessage = false;
        EventSystem.current.SetSelectedGameObject(null);
        InputManager playerInput = FindObjectOfType<InputManager>();
        if (playerInput != null)
        {
            playerInput.EnablePlayerControls();
            playerInput.EnableUIControls();
        }
        backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
        //isActive = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (EndDialogueEvent != null)
            EndDialogueEvent.Invoke();
    }

    public void PreventDeselection()
    {

            if(EventSystem.current.currentSelectedGameObject != nextButton.gameObject && EventSystem.current.currentSelectedGameObject != skipButton.gameObject)
            {
                EventSystem.current.SetSelectedGameObject(nextButton.gameObject);
            }

    }

    private void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;   
    }
    private void Update() 
    {
        if(isDisplayingMessage && backgroundBox.transform.localScale != Vector3.zero)
        {
            PreventDeselection();
        }
    }

}
