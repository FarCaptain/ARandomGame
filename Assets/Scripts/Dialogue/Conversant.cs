using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversant : Interactable
{
    public DialogueDisplay dialogueDisplay;

    public override void Interact()
    {
        base.Interact();
        dialogueDisplay.StartConversation();
    }
}
