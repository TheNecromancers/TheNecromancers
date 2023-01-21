using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogueOnLoad : MonoBehaviour
{
    private void Start()
    {
        DialogueTrigger dialogue = FindObjectOfType<DialogueTrigger>();
        if(dialogue != null)
        {
            dialogue.StartDialogue();
        }
    }
}
