using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventManager : MonoBehaviour
{
    #region Singleton

    public static DialogueEventManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    
    public DialogueManager dialogueManager;

    public delegate void DialogueEventShooter();

    public DialogueEventShooter answer1EventTriggered;
    public DialogueEventShooter answer2EventTriggered;
    public DialogueEventShooter dialogueEndEventTriggered;

    void Start()
    {
        dialogueManager = GetComponent<DialogueManager>();
    }

    public void Answer1Event()
    {

        answer1EventTriggered?.Invoke();

    }

    public void Answer2Event() 
    {
        answer2EventTriggered?.Invoke();
    }
}
