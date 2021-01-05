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
        // Jump to the line at the specified index
        Jump,
        // Line to display when making choice
        StartChoice,
        Choice,
        // Indicate that choice has been listed out
        ChoiceEnd,
        // Return with the string specified
        Return,
    }
    public List<(DialogueLineType, string, int)> dialogueLines;
}
