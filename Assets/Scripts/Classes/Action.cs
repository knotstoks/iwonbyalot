using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Action
{
    public enum ActionType
    {
        Farming,
        TradingVotes,
        Research,
    }
    public readonly string actionName;
    public readonly string description;
    public readonly int influenceGain;
    public readonly int moneyGain;
    public readonly ActionType actionType;

    public Action(ActionData actionData)
    {
        this.actionName = actionData.actionName;
        this.description = actionData.description;
        this.influenceGain = actionData.influenceGain;
        this.moneyGain = actionData.moneyGain;
        this.actionType = actionData.actionType;
    } 
}
