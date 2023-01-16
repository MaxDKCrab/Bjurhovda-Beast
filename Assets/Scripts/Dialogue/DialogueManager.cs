using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region Singleton

    public static DialogueManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    
    
    
    
    private ChoiceDialogue choiceDialogue;
    public GameObject dialogueUI;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI answer1Text;
    public TextMeshProUGUI answer2Text;
    public GameObject choices;
    public Image charIcon;
    // public GameObject choiceText;
    // public GameObject continueText;
    
    private int dialogueTracker;
    [HideInInspector] public bool inChoice;
    [HideInInspector] public bool isTalking;
    private int answerNum = 0;
    private bool isInMessage;

    public float timeBetweenLetters;
    

    void Start()
    {
        dialogueTracker = 0; 
        dialogueUI.SetActive(false);
    }

    public void StartDialogue(ChoiceDialogue choiceDia)
    {
        choiceDialogue = choiceDia;
        choiceDialogue.isDialogueFinished = false;
        
        isTalking = true;
        
        dialogueUI.SetActive(true);
        
        answer1Text.text = choiceDialogue.answer1;
        answer2Text.text = choiceDialogue.answer2;
        DisplayNextSentenceChoice();
        PlayerManager.instance.GamePause(true);

    }

    public void StartReturnMessageDialogue(ChoiceDialogue choice)
    {
        
        choiceDialogue = choice;
        isTalking = true;
        dialogueUI.SetActive(true);
        DisplayReturnMessage();
        PlayerManager.instance.GamePause(true);

    }
    

    public void DisplayNextSentenceChoice()
    {
        if (choiceDialogue.linesInitial.Length == 1)
        {
            DisplayReturnMessage();
            return;
        }
        
        if (answerNum == 1 && dialogueTracker >= choiceDialogue.linesBranch1.Length)
        {
            EndDialogue();
            return;
        }
        if (answerNum == 2 && dialogueTracker >= choiceDialogue.linesBranch2.Length)
        {
            EndDialogue();
            return;
        }
        if (answerNum == 0 && dialogueTracker >= choiceDialogue.linesInitial.Length)
        {
            EndDialogue();
            return;
        }

        string sentence;
        
        if (answerNum == 1)
        {
            nameText.text = choiceDialogue.linesBranch1[dialogueTracker].character.charName;
            SetPortraitLineArray(choiceDialogue.linesBranch1);
            sentence = choiceDialogue.linesBranch1[dialogueTracker].text;
        }
        else if (answerNum == 2)
        {
            nameText.text = choiceDialogue.linesBranch2[dialogueTracker].character.charName;
            SetPortraitLineArray(choiceDialogue.linesBranch2);
            sentence = choiceDialogue.linesBranch2[dialogueTracker].text;
        }
        else
        {
            nameText.text = choiceDialogue.linesInitial[dialogueTracker].character.charName;
            SetPortraitChoiceArray(choiceDialogue.linesInitial);
            sentence = choiceDialogue.linesInitial[dialogueTracker].text;
        }
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        if (answerNum == 0 && choiceDialogue.linesInitial[dialogueTracker].isChoiceTrigger)
        {
            // continueText.SetActive(false);
            // choiceText.SetActive(true);
            inChoice = true;
            choices.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        dialogueTracker++;
        
    }

    public void Answer1()
    {
        // continueText.SetActive(true);
        // choiceText.SetActive(false);
        answerNum = 1;
        dialogueTracker = 0;
        inChoice = false;
        choices.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        DisplayNextSentenceChoice();
    }
    public void Answer2()
    {
        // continueText.SetActive(true);
        // choiceText.SetActive(false);
        answerNum = 2;
        dialogueTracker = 0;
        inChoice = false;
        choices.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        DisplayNextSentenceChoice();
    }
    
    
    public void DisplayReturnMessage()
    {
        if (isInMessage)
        {
            EndDialogue();
            isInMessage = false;
            return;
        }
        string sentence;
        
        nameText.text = choiceDialogue.onReturnDialogue.character.charName;
        SetPortraitLine(choiceDialogue.onReturnDialogue);
        sentence = choiceDialogue.onReturnDialogue.text;
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
        isInMessage = true;
    }

    IEnumerator TypeSentence (string sentence)
    {
        speechText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
    }

    private void SetPortraitLineArray(Line[] branch)
    {
        charIcon.sprite = branch[dialogueTracker].character.icon;
    }

    private void SetPortraitLine(Line returnMessage)
    {
        charIcon.sprite = returnMessage.character.icon;
    }

    private void SetPortraitChoiceArray(ChoiceLine[] choiceBranch)
    {
        charIcon.sprite = choiceBranch[dialogueTracker].character.icon;
    }
    
    
    public void EndDialogue()
    {
        choiceDialogue.isDialogueFinished = true;
        answerNum = 0;
        isTalking = false;
        dialogueUI.SetActive(false);
        dialogueTracker = 0;
        choiceDialogue.talking = false;
        PlayerManager.instance.GamePause(false);
    }
}
