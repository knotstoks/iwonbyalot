using Sys_Random = System.Random;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int influence;
    public int money;
    public int[] possible_messages;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Faction
{
    private static readonly Sys_Random rnd = new Sys_Random();
    private List<string> liked_msgs;
    private List<string> disliked_msgs;

    public Faction(List<string> possible_msgs, int liked_msg_count, int disliked_msg_count)
    {
        List<string> shuffled = possible_msgs.OrderBy(x => rnd.Next()).ToList();
        liked_msgs = shuffled.GetRange(0, liked_msg_count);
        disliked_msgs = shuffled.GetRange(liked_msg_count, disliked_msg_count);
    }
}

