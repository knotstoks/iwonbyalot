using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelData : ScriptableObject
{
    public string position;
    public string defaultBackground;
    public int startTime, endTime;
    public int days;
    public int districtCount;
    public int forVotesStartPerDistrict;
    public int totalVotesPerDistrict;
    public GameObject mapContainer;
    public DialogueEvent introductionDialogue;
    public List<ActionData> actionSlots;
    public List<GameObject> resources;
    public List<RandomDialogueEvent> NormalEvents;
    public List<RandomDialogueEvent> HardcoreEvents;
    public List<CampaignMessage> campaignMessages;
}
