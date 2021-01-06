using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelData : ScriptableObject
{
    public string position;
    public int startTime, endTime;
    public int days;
    public List<ActionData> actionSlots;
    public List<RandomDialogueEvent> NormalEvents;
    public List<RandomDialogueEvent> HardcoreEvents;
}
