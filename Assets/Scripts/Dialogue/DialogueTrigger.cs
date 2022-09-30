using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public float dialogueRange = 100f;
    private DialogueManager dialogueManager;
    public ChoiceDialogue choiceDialogue;
    private float distance;
    private GameObject player;
    // private InteractionManager interact;
    // public string interactMessage;
    private bool withinRange;

    void Start()
    {
        // interact = FindObjectOfType<InteractionManager>();
        player = FindObjectOfType<Movement>().gameObject;
        choiceDialogue.isDialogueFinished = false;
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // interact.ShowInteractMessage(interactMessage);
        withinRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        // interact.HideInteractMessage();
        withinRange = false;
        if (dialogueManager.isTalking && choiceDialogue.talking)
        {
            dialogueManager.CancelDialogue();
            choiceDialogue.talking = false;
        }
    }

    private void Update()
    {
        if (!withinRange)
        {
            return;
        }
        distance = Vector3.Distance(player.transform.position, transform.position);
        
        if (distance <= dialogueRange && Input.GetKeyDown(KeyCode.E))
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
            else if (dialogueManager.isTalking)
            {
                if (!dialogueManager.inChoice && choiceDialogue.isDialogueFinished)
                {
                    dialogueManager.DisplayReturnMessage();
                }
                else if (!dialogueManager.inChoice && !choiceDialogue.isDialogueFinished)
                {
                    dialogueManager.DisplayNextSentenceChoice();
                }
            }
        }
        
    }
    
}
