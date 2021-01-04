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
    private int influenceGain;
    private int moneyGain;
    private ActionType actionType;
    public int districtChosen;

    public Action(int influenceGain, int moneyGain, ActionType actionType)
    {
        this.influenceGain = influenceGain;
        this.moneyGain = moneyGain;
        this.actionType = actionType;
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
