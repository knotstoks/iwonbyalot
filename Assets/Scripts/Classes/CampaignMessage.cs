using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Campaign Message", menuName = "ScriptableObjects/CampaignMessage", order = 1)]
public class CampaignMessage : ScriptableObject
{
    public string messageName;
    public string speech;
    public string like;
    public string neutral;
    public string dislike;
    public Sprite icon;
}
