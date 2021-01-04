using Sys_Random = System.Random;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class District
{
    private static readonly Sys_Random rnd = new Sys_Random();
    public List<string> likedMsgs;
    public List<string> dislikedMsgs;
    public int forVotesCount = 10;
    public int againstVotesCount = 9;

    public District(List<string> possible_msgs, int liked_msg_count, int disliked_msg_count)
    {
        List<string> shuffled = possible_msgs.OrderBy(x => rnd.Next()).ToList();
        likedMsgs = shuffled.GetRange(0, liked_msg_count);
        dislikedMsgs = shuffled.GetRange(liked_msg_count, disliked_msg_count);
    }

    public District()
    {
        likedMsgs = new List<string>();
        dislikedMsgs = new List<string>();
    }
}
