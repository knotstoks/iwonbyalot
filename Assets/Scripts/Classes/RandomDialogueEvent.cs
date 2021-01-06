using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RandomDialogueEvent", menuName = "ScriptableObjects/RandomDialogueEvent", order = 1)]
public class RandomDialogueEvent : ScriptableObject
{
    public enum TriggerVarType
    {
        None,
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
    public int triggerValue;
    // Chance of triggering the event. Higher means more likely. 0 is guarenteed.
    public int triggerChance;

    public bool CompareValue(int other)
    {
        switch (comparisonType)
        {
            case ComparisonType.Less:
                return other < triggerValue;
            case ComparisonType.Equal:
                return other == triggerValue;
            case ComparisonType.Greater:
                return other > triggerValue;
        }
        return false;
    }
}
