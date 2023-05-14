using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerZone : MonoBehaviour
{
    private Collider thisColl;
    private DialogueManager dialogueManager;
    public ChoiceDialogue choiceDialogue;
    
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        thisColl = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        dialogueManager.StartDialogue(choiceDialogue);
        choiceDialogue.talking = true;
        thisColl.enabled = false;
    }

    void Update()
    {
        if (dialogueManager.isTalking && Input.GetKeyDown(KeyCode.E) && choiceDialogue.talking && !dialogueManager.inChoice)
        {
            dialogueManager.DisplayNextSentenceChoice();
        }
    }
}
