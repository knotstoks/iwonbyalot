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
    private string name;
    private string description;
    private int influenceGain;
    private int moneyGain;
    private ActionType actionType;
    public int districtChosen;

    public Action(string name, string description, int influenceGain, 
        int moneyGain, ActionType actionType)
    {
        this.name = name;
        this.description = description;
        this.influenceGain = influenceGain;
        this.moneyGain = moneyGain;
        this.actionType = actionType;
    }

    public string getName()
    {
        return name;
    }

    public string getDescription()
    {
        return description;
    }

    public int getInfluenceGain()
    {
        return influenceGain;
    }

    public int getMoneyGain()
    {
        return moneyGain;
    }

    public ActionType getActionType()
    {
        return actionType;
    }
}
