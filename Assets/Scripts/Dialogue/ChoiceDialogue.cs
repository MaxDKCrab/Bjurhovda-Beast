using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "New Choice Dialogue", menuName = "Choice Dialogue Object")]
public class ChoiceDialogue : ScriptableObject
{
    public ChoiceLine[] linesInitial;
    public string answer1;
    public Line[] linesBranch1;
    public string answer2;
    public Line[] linesBranch2;
    public Line onReturnDialogue;
    [HideInInspector] public bool isDialogueFinished;
    [HideInInspector] public bool talking;
}


[System.Serializable] public class Line
{
    public Characters character;
    [TextArea(3, 10)] public string text;
}


[System.Serializable] public class ChoiceLine
{
    public Characters character;
    [TextArea(3, 10)] public string text;
    public bool isChoiceTrigger;
}

