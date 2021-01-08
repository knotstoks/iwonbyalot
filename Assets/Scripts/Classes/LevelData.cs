using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelData : ScriptableObject
{
    public int level;
    
    public string position;
    public string defaultBackground;
    public int likeVoteGain,neutralVoteGain,dislikeVoteGain;
    
    public int startTime, endTime;
    public int days;
    public List<string> districtNames;
    public int opponentGainPerRound;
    public int forVotesStart;
    public int totalVotes;
    public bool messageEnabled;
    public bool advancedResourcesEnabled;
    public GameObject mapContainer;
    public DialogueEvent introductionDialogue;
    
    public List<ActionData> actionSlots;
    public List<GameObject> resources;
    public List<RandomDialogueEvent> NormalEvents;
    public List<RandomDialogueEvent> HardcoreEvents;
    public List<CampaignMessage> campaignMessages;
}
