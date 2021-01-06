using SysRandom = System.Random;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class District
{
    private static readonly SysRandom rnd = new SysRandom();
    public string name;
    public List<int> likedMsgs;
    public List<int> dislikedMsgs;
    public int forVotesCount = 10;
    public int againstVotesCount = 9;

    public District(List<int> possible_msgs, int liked_msg_count, int disliked_msg_count)
    {
        List<int> shuffled = possible_msgs.OrderBy(x => rnd.Next()).ToList();
        likedMsgs = shuffled.GetRange(0, liked_msg_count);
        dislikedMsgs = shuffled.GetRange(liked_msg_count, disliked_msg_count);
    }

    public District()
    {
        likedMsgs = new List<int>();
        dislikedMsgs = new List<int>();
    }
}
