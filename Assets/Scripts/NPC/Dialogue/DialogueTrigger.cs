using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
    private DialogueManager dialogueManager;
    private GamePauseInput pauseInput;
    public ChoiceDialogue choiceDialogue;
    private bool withinRange;

    void Start()
    {
        choiceDialogue.isDialogueFinished = false;
        dialogueManager = DialogueManager.instance;
        pauseInput = GamePauseInput.instance;
    }

    protected override void Interact()
    {
        if (!dialogueManager.isTalking && !choiceDialogue.isDialogueFinished)
        {
            dialogueManager.StartDialogue(choiceDialogue);
            choiceDialogue.talking = true;
        }
        else if (!dialogueManager.isTalking && choiceDialogue.isDialogueFinished)
        {
            dialogueManager.StartReturnMessageDialogue(choiceDialogue);
            choiceDialogue.talking = true;
        }
    }

    private void Update()
    {

        
        
    }
    
}
