using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RandomDialogueEvent", menuName = "ScriptableObjects/RandomDialogueEvent", order = 1)]
public class RandomDialogueEvent : ScriptableObject
{
    public enum TriggerVarType
    {
        Influence,
        Day,
    }
    public enum ComparisonType
    {
        Less,
        Equal,
        Greater,
    }
    public DialogueEvent dialogueEvent;
    public TriggerVarType triggerVarType;
    public ComparisonType comparisonType;
    public int comparedTo;
    // Chance of triggering the event. Higher means more likely. 0 is guarenteed.
    public int triggerChance;
}
