using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;

public class DödaPåDialogueChoice : MonoBehaviour
{
    public DialogueEventManager dialogueEventManager;
    public Damagable damagable;
    public DialogueTrigger myDialogue;

    public int answerTriggerNum;

    public bool skaHanDö;


    private void Start()
    {
        myDialogue = GetComponent<DialogueTrigger>();
        damagable = GetComponent<Damagable>();
        dialogueEventManager = DialogueEventManager.instance;

        if (answerTriggerNum == 1)
        {
            dialogueEventManager.answer1EventTriggered += killOption;
        }
        else if ( answerTriggerNum == 2)
        {
            dialogueEventManager.answer2EventTriggered += killOption;
        }

        dialogueEventManager.dialogueEndEventTriggered += DOIT;

    }

    private void OnDisable()
    {
        if (answerTriggerNum == 1)
        {
            dialogueEventManager.answer1EventTriggered -= killOption;
        }
        else if (answerTriggerNum == 2)
        {
            dialogueEventManager.answer2EventTriggered -= killOption;
        }

        dialogueEventManager.dialogueEndEventTriggered -= DOIT;

    }

    public void killOption()
    {
        if (myDialogue.choiceDialogue == dialogueEventManager.dialogueManager.choiceDialogue)
        {
            skaHanDö = true;
        }
    }


    public void DOIT()
    {
        if (skaHanDö)
        {
            StartCoroutine(nameof(delayKill));
        }


    }


    private IEnumerator delayKill()
    {
        yield return new WaitForSeconds(1f);

        damagable.Die();
    }
}
