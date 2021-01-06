using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueEvent", menuName = "ScriptableObjects/DialogueEvent", order = 1)]
public class DialogueEvent : ScriptableObject
{
    public enum DialogueLineType
    {
        // Normal Text
        Text,
        // Uses String Format.
        Formated,
        // Gets a random district to use
        RandomDistrict,
        // Jump to the line at the specified index
        Jump,
        // Line to display when making choice
        StartChoice,
        // A choice which can be chosen
        Choice,
        // The last choice which can be chosen
        ChoiceEnd,
        // Return with the string specified
        Return,
    }
    public List<DialogueLineType> dialogueLineTypes;
    public List<string> dialogueLines;
    public List<int> dialogueLineLinks;
}
