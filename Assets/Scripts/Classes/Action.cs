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
    public readonly int districtChosen = -1;

    public Action(ActionData actionData)
    {
        this.actionName = actionData.actionName;
        this.description = actionData.description;
        this.influenceGain = actionData.influenceGain;
        this.moneyGain = actionData.moneyGain;
        this.actionType = actionData.actionType;
    } 

    public Action CopyWithDistrict(int districtIndex)
    {
        return new Action(actionName, description, influenceGain, moneyGain, actionType, districtIndex);
    }

    private Action(string name, string description, int influenceGain,
        int moneyGain, ActionType actionType, int districtChosen)
    {
        this.actionName = name;
        this.description = description;
        this.influenceGain = influenceGain;
        this.moneyGain = moneyGain;
        this.actionType = actionType;
        this.districtChosen = districtChosen;
    }
}
