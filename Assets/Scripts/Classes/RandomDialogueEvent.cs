using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RandomDialogueEvent", menuName = "ScriptableObjects/RandomDialogueEvent", order = 1)]
public class RandomDialogueEvent : ScriptableObject
{
    public enum TriggerVar
    {
        Influence,
        Day,
    }
    public enum Comparison
    {
        Less,
        Equal,
        Greater,
    }
    public DialogueEvent dialogueEvent;
    public (TriggerVar, Comparison, int) triggerConditions;
    // Chance of triggering the event. Higher means more likely. 0 is guarenteed.
    public int triggerChance;
}
