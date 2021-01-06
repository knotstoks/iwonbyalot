using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "ScriptableObjects/Action", order = 1)]
public class ActionData : ScriptableObject
{
    public DialogueEvent dialogueEvent;
    public string actionName;
    public string description;
    public int influenceGain;
    public int moneyGain;
    public Action.ActionType actionType;
}
