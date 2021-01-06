using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Random = UnityEngine.Random;

public class Tracker : MonoBehaviour
{
	public List<GameObject> schedulingUi;
	public GameObject Desc, actionslotPrefab, timeslotPrefab, Execute;
    public GameObject InfluenceSlider, MoneySlider, TimeslotContainer, ActionContainer;
	public Text InfluenceStat,MoneyStat;
	
	private List<GameObject> timeslots;
	private List<GameObject> actionslots;
	private List<Action> currentSchedule;
    private List<ActionData> actions;
    private List<RandomDialogueEvent> randomDialogueEvents;

	public float eventChance = 0.5f;
    public float money;
    public float influence;
	public int forVotesCount, againstVotesCount;
	private int selectedTimeslot;
	private int days;
    
    public LevelData level_data;


	// Start is called before the first frame update
	void Start()
    {
        level_data = DataPassedToMainGame.level_data;
        
		timeslots = new List<GameObject>();
		currentSchedule = new List<Action>();
        actionslots = new List<GameObject>();
        
        switch (DataPassedToMainGame.diff) {
            case 0:
                randomDialogueEvents = level_data.NormalEvents;
                break;
            case 1:
                randomDialogueEvents = level_data.HardcoreEvents;
                break;
        }
        
        actions = level_data.actionSlots;
        days = level_data.days;
        Init(level_data.startTime, level_data.endTime);
        
		selectedTimeslot = -1;
		Reset();
	}
	
	void Init(int start_hour, int end_hour) {
		for (int i = start_hour; i < end_hour; i++) {
			GameObject timeslot = Instantiate(timeslotPrefab, TimeslotContainer.transform) as GameObject;
			timeslot.GetComponent<TimeSlotter>().Init(i - start_hour, i, this);
			timeslots.Add(timeslot);
			currentSchedule.Add(null);
		}
        
		for (int i = 0; i < actions.Count; i++)
		{
			GameObject actionslot = Instantiate(actionslotPrefab, ActionContainer.transform) as GameObject;
			actionslot.GetComponent<ActionSlotter>().Init(actions[i], this, Desc);
			actionslots.Add(actionslot);
		}
        
		LayoutRebuilder.ForceRebuildLayoutImmediate(ActionContainer.GetComponent<RectTransform>());
	}

	void NextLevel() {
		SceneManager.LoadScene(SceneManager.sceneCount +1);
	}

	public void SelectTimeslot(int index)
    {
		if (selectedTimeslot != -1)
        {
			timeslots[selectedTimeslot].GetComponent<TimeSlotter>().SetAction(null);
		}
		selectedTimeslot = index;
	}

	public void SelectAction(Action action)
	{
		if (selectedTimeslot != -1)
		{
			currentSchedule[selectedTimeslot] = action;
			timeslots[selectedTimeslot].GetComponent<TimeSlotter>().SetAction(action);
			selectedTimeslot = -1;
		}
	}

    // Update is called once per frame
    public void Reset()
    {
        for (int i = 0; i < timeslots.Count; i++)
        {
			timeslots[i].GetComponent<TimeSlotter>().Reset();
			currentSchedule[i] = null;
		}

        Desc.GetComponent<Description>().days = days;
		Desc.GetComponent<Description>().ratio = String.Format("{0} / {1}",forVotesCount,againstVotesCount);
		Desc.GetComponent<Description>().SetOriginalText();
		MoneyStat.text = money.ToString() + "/ 100";
		InfluenceStat.text = influence.ToString() + " /100";
		MoneySlider.GetComponent<Slider>().value = money;
		InfluenceSlider.GetComponent<Slider>().value = influence; 
		
        Execute.GetComponent<Button>().interactable = false;
    }

    public void UpdateExecute() {
        // check if all timeslots have been set
        for (int i = 0; i < timeslots.Count; i++) {
			if (currentSchedule[i] == null)
				return;
		}
		
        Execute.GetComponent<Button>().interactable = true;
    }
	
	public void ExecuteAction() {
		DisableSchedulingUi();
		days -= 1;

		List<string> dialogueList = new List<string>();
		for (int i = 0; i < currentSchedule.Count; i++)
		{	
			string dialogue;
			Action action = currentSchedule[i];
			if( money + action.moneyGain >= 0 && influence + action.influenceGain >= 0) 
			{ 
				influence += action.influenceGain;
				money += action.moneyGain;

				//switch (action.getActionType())
				//{
				//    case Action.ActionType.Farming:

				//}
				dialogue = String.Format("Did {0} to gain", action.actionName);
				if (action.influenceGain > 0)
                {
					dialogue += String.Format(" {0} influence", action.influenceGain);
				}
				if (action.moneyGain > 0)
				{
					dialogue += String.Format(" and {0} money", action.moneyGain);
				}
				if(action.actionType == Action.ActionType.TradingVotes) {
					dialogue += " votes";
					forVotesCount += 10;
					againstVotesCount -= 10;
				}
				
			}
			else if (money + action.moneyGain < 0 && influence + action.influenceGain >= 0)
			{
				dialogue = String.Format("You do not have enough money to do {0}",action.actionName);
			}
			else if (money + action.moneyGain >= 0 && influence + action.influenceGain < 0)
			{
				dialogue = String.Format("You do not have enough influence to do {0}",action.actionName);
			} else 
			{
				dialogue = String.Format("You do not have enough money nor influence to do {0}",action.actionName);
			}
			dialogueList.Add(dialogue);
		}
		
		if  (forVotesCount - 3 < 0)
		{
			againstVotesCount += forVotesCount;
			forVotesCount = 0;
		} 
		else 
		{
			forVotesCount -= 3;
			againstVotesCount += 3;
		}

		if (days == 0) 
		{
			string announcement;
			if(forVotesCount >= againstVotesCount) 
			{
				announcement = String.Format(" Congratulations! You have won the election with a result of {0} against {1}",forVotesCount,againstVotesCount); 
			}
			else 
			{
				announcement = String.Format("You have lost the election with a result of {0} against {1}. Try harder next time.",forVotesCount,againstVotesCount);
			}
			dialogueList.Add(announcement);
			//NextLevel();
		}
		Desc.GetComponent<Description>().ExecuteActionsDialogue(dialogueList, ResumeScheduling);
	}

	public void ExecuteRandomEvent()
	{
		DisableSchedulingUi();
		List<RandomDialogueEvent> available = new List<RandomDialogueEvent>();
		foreach(RandomDialogueEvent randomEvent in randomDialogueEvents)
        {
			switch (randomEvent.triggerVarType)
            {
				case RandomDialogueEvent.TriggerVarType.None:
					available.Add(randomEvent);
					break;
				case RandomDialogueEvent.TriggerVarType.Day:
					if (randomEvent.CompareValue(days))
                    {
						available.Add(randomEvent);
                    }
					break;
			}
		}
		if (available.Count == 0)
        {
			EndRandomEvent("");
			return;
		}

		int totalChance = 0;
		foreach (RandomDialogueEvent randomEvent in available)
		{
			if (randomEvent.triggerChance == 0)
            {
				Desc.GetComponent<EventExecuter>().ExecuteEventDialogue(
					randomEvent.dialogueEvent,
					makeCurrentStatistics(),
					EndRandomEvent
					);
				return;
			}
			totalChance += randomEvent.triggerChance;
		}
		if (Random.Range(0.0f, 1.0f) > eventChance)
        {
			EndRandomEvent("");
			return;
        }
		int choosenChance = Random.Range(0, totalChance);
		foreach (RandomDialogueEvent randomEvent in available)
		{
			choosenChance -= randomEvent.triggerChance;
			if (choosenChance < 0)
			{
				Desc.GetComponent<EventExecuter>().ExecuteEventDialogue(
					randomEvent.dialogueEvent,
					makeCurrentStatistics(),
					EndRandomEvent
					);
				return;
			}
		}

	}

	public void EndRandomEvent(string result)
    {
		if (!result.Equals(""))
        {
			ParseRandomEventResult(result);
		}
		ExecuteAction();
		
	}

	public void ParseRandomEventResult(string result)
    {
		List<string> effects = result.Split(',').ToList();
		foreach (string effect in effects)
		{
			List<string> effectWords = effect.Split(' ').ToList();
			switch (effectWords[0])
			{
				case "inf":
					influence += int.Parse(effectWords[1]);
					if (influence < 0) influence = 0;
					break;
			}
		}
	}


	public CurrentStatistics makeCurrentStatistics()
    {
		CurrentStatistics result = new CurrentStatistics();
		result.districts = new List<District>();
		result.influence = influence;
		result.money = money;
		result.days = days;
		return result;
    }

	public void ResumeScheduling()
    {
		foreach (GameObject ui in schedulingUi)
		{
			ui.SetActive(true);
		}
		Reset();
	}

	private void DisableSchedulingUi()
    {
		foreach (GameObject ui in schedulingUi)
        {
			ui.SetActive(false);
        }
    }
}
