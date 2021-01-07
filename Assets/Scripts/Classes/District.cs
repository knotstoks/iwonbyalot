using SysRandom = System.Random;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class District
{
    private static readonly SysRandom rnd = new SysRandom();
    public string name = "default name";
    public List<int> likedMsgs;
    public List<int> dislikedMsgs;
    public int forVotesCount ;
    public int totalVotesCount ;
    public List<int> neutralMsgs;
    public int dislikedMsgsFound = 0;
    public int neutralMsgsFound = 0;

    public District(List<int> possible_msgs, int liked_msg_count, int disliked_msg_count)
    {
        List<int> shuffled = possible_msgs.OrderBy(x => rnd.Next()).ToList();
        likedMsgs = shuffled.GetRange(0, liked_msg_count);
        dislikedMsgs = shuffled.GetRange(liked_msg_count, disliked_msg_count);
        neutralMsgs = shuffled.GetRange(disliked_msg_count, possible_msgs.Count);
    }

    public District()
    {
        likedMsgs = new List<int>();
        dislikedMsgs = new List<int>();
        neutralMsgs = new List<int>();
    }
}
