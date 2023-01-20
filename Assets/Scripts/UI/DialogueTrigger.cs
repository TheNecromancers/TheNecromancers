using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;
    [SerializeField] UnityEvent OnEndDialogue;

    public void StartDialogue()
    {
        DialogueManager.Instance.OpenDialogue(messages, actors,OnEndDialogue);
    }
}

[System.Serializable]
public class Message
{
    public int actorId;
    [TextArea]
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}
