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
    public int opponentGainPerRound;

    public District(List<int> possibleMsgs, int likedMsgCount, int dislikedMsgCount)
    {
        List<int> shuffled = possibleMsgs.OrderBy(x => rnd.Next()).ToList();
        likedMsgs = shuffled.GetRange(0, likedMsgCount);
        dislikedMsgs = shuffled.GetRange(likedMsgCount, dislikedMsgCount);
        int likedDislikedCount = likedMsgCount + dislikedMsgCount;
        neutralMsgs = shuffled.GetRange(likedDislikedCount, possibleMsgs.Count - likedDislikedCount);
    }

    public District()
    {
        likedMsgs = new List<int>();
        dislikedMsgs = new List<int>();
        neutralMsgs = new List<int>();
    }
}
