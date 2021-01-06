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
        // Changes the background (not implemented yet)
        Background,
        // Uses String Format (not implemented yet).
        Formated,
        // Gets a random district to use
        RandomDistrict,
        // Jump to the line at the specified index
        Jump,
        // Line to display when making choice
        // Links corresponding to Choice and ChoiceEnd will denote the index to jump to if selected
        // Choices will be displayed based on incrementing the index by 1 until ChoiceEnd is seen.
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
