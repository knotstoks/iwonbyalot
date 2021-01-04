using Sys_Random = System.Random;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class District
{
    private static readonly Sys_Random rnd = new Sys_Random();
    public List<string> liked_msgs;
    public List<string> disliked_msgs;
    public int for_votes_count = 10;
    public int against_votes_count = 9;

    public District(List<string> possible_msgs, int liked_msg_count, int disliked_msg_count)
    {
        List<string> shuffled = possible_msgs.OrderBy(x => rnd.Next()).ToList();
        liked_msgs = shuffled.GetRange(0, liked_msg_count);
        disliked_msgs = shuffled.GetRange(liked_msg_count, disliked_msg_count);
    }

    public District()
    {
        liked_msgs = new List<string>();
        disliked_msgs = new List<string>();
    }
}
