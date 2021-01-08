using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Action", menuName = "ScriptableObjects/Action", order = 1)]
public class ActionData : ScriptableObject
{
    public enum ActionType {
        Farming,
        TradingVotes,
        Research
    }
    
    public Sprite icon;
    public string actionName;
    public string description;
    public int influenceGain;
    public int moneyGain;
    public int stressGain;
    public int charismaGain;
    
    public ActionType actionType;
}
