using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int influence;
    public int money;
    public int messageCount;
    public int dayCount;
    public int totalDays;
    public List<string> messageStrings;
    public List<District> districts;
    public List<Action> currentSchedule;

    void initLevelOne()
    {
        influence = 0;
        money = 0;
        messageCount = 10;
        messageStrings = new List<string>(new string[] { "a", "b", "c", "d", "e", "f", "h", "i", "j" });
        List<int> tempList = Enumerable.Range(0, 10).ToList();
        District dist1 = new District(tempList, 3, 3);
        districts = new List<District>();
        districts.Add(dist1);
        currentSchedule = new List<Action>();
        currentSchedule.Add(null);
        currentSchedule.Add(null);
        currentSchedule.Add(null);
        currentSchedule.Add(null);
        dayCount = 0;
        totalDays = 10;
    }

    void assignToSchedule(int timeslot, Action action)
    {
        currentSchedule[timeslot] = action;
    }

    void executeSchedule()
    {
        for (int i = 0; i < currentSchedule.Count; i++)
        {
            Action action = currentSchedule[i];
            influence += action.influenceGain;
            money += action.moneyGain;
            //switch (action.getActionType())
            //{
            //    case Action.ActionType.Farming:

            //}
            dayCount += 1;
        }
    }

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
