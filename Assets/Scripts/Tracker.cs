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
	public GameObject Desc, actionslotPrefab, timeslotPrefab, executeButton, 
		tutorialPrefab, TimeslotContainer, ActionContainer, ResourceContainer;
    private GameObject InfluenceSlider, MoneySlider, StressSlider, CharismaSlider, tutorial;
    
	private List<RandomDialogueEvent> randomDialogueEvents;
    private List<ActionData> actions;
	private List<GameObject> resourceslots;
	private List<GameObject> timeslots;
	private List<GameObject> actionslots;

	private List<ActionData> currentSchedule;
	private List<int> actionDistrictTarget;
	private List<int> actionMessageTarget;

	private bool isUsingDistricts;
	private List<District> districts;
	private GameObject mapContainer;

	public float eventChance = 0.5f;
    private float money, influence, stress, charisma;
	private int forVotesCount, againstVotesCount;
	private int selectedTimeslot;
	private int days;
    
    private LevelData levelData;
	public LevelData testLevelData;


	// Start is called before the first frame update
	void Start()
    {
        levelData = DataPassedToMainGame.level_data;
		if (levelData == null)
        {
			levelData = testLevelData;
		}
        
		timeslots = new List<GameObject>();
		currentSchedule = new List<ActionData>();
        actionslots = new List<GameObject>();

		actionDistrictTarget = new List<int>();
		actionMessageTarget = new List<int>();

		switch (DataPassedToMainGame.diff) {
            case 0:
                randomDialogueEvents = levelData.NormalEvents;
                break;
            case 1:
                randomDialogueEvents = levelData.HardcoreEvents;
                break;
        }
        
        actions = levelData.actionSlots;
        days = levelData.days;
		resourceslots = levelData.resources;
        forVotesCount = levelData.forVotesStart;
        againstVotesCount = levelData.againstVotesStart;

		isUsingDistricts = levelData.districtCount > 0;
		if (isUsingDistricts)
        {
			mapContainer = levelData.mapContainer;
			districts = new List<District>();
			for (int i = 0; i < levelData.districtCount; i++)
			{
				districts.Add(new District());
			}
        }
		
        Init(levelData.startTime, levelData.endTime);
        
		selectedTimeslot = -1;
		Reset();
    
		if (levelData.introductionDialogue != null)
        {
			DisableSchedulingUi();
			Desc.GetComponent<EventExecuter>().ExecuteEventDialogue(
						levelData.introductionDialogue,
						makeCurrentStatistics(),
						ResumeGameStart
						);
        }	
	}

	public void ResumeGameStart(string result)
	{
		foreach (GameObject ui in schedulingUi)
		{
			ui.SetActive(true);
		}
		Reset();

		if (DataPassedToMainGame.tutorial)
		{
			tutorial = Instantiate(tutorialPrefab, this.transform) as GameObject;
			tutorial.GetComponent<Tutorial>().Init(this);
		}
	}

	void Init(int start_hour, int end_hour) {
		for (int i = start_hour; i < end_hour; i++) {
			GameObject timeslot = Instantiate(timeslotPrefab, TimeslotContainer.transform) as GameObject;
			timeslot.GetComponent<TimeSlotter>().Init(i - start_hour, i, this);
			timeslots.Add(timeslot);
			currentSchedule.Add(null);
			actionDistrictTarget.Add(-1);
			actionMessageTarget.Add(-1);
		}
        
		for (int i = 0; i < actions.Count; i++)
		{
			GameObject actionslot = Instantiate(actionslotPrefab, ActionContainer.transform) as GameObject;
			actionslot.GetComponent<ActionSlotter>().Init(actions[i], this, Desc);
			actionslots.Add(actionslot);
		}

		for(int i = 0; i < resourceslots.Count; i++)
		{
			Instantiate(resourceslots[i],ResourceContainer.transform);
		}
        MoneySlider = GameObject.FindWithTag("Money");
		InfluenceSlider = GameObject.FindWithTag("Influence");
		StressSlider = GameObject.FindWithTag("Stress");
		CharismaSlider = GameObject.FindWithTag("Charisma");
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

	public void SelectAction(ActionData action)
	{
		if (selectedTimeslot != -1)
		{
			currentSchedule[selectedTimeslot] = action;
			timeslots[selectedTimeslot].GetComponent<TimeSlotter>().SetAction(action);
			selectedTimeslot = -1;
		}
	}

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
		MoneySlider.GetComponentInChildren<Text>().text = money.ToString() + "/ 100";
		InfluenceSlider.GetComponentInChildren<Text>().text = influence.ToString() + " /100";
		MoneySlider.GetComponent<Slider>().value = money;
		InfluenceSlider.GetComponent<Slider>().value = influence; 
		
        executeButton.GetComponent<Button>().interactable = false;
    }

    public void UpdateExecute() {
        // check if all timeslots have been set
        for (int i = 0; i < timeslots.Count; i++) {
			if (currentSchedule[i] == null)
				return;
		}
		
        executeButton.GetComponent<Button>().interactable = true;
    }
    
    public string MakeGainDialogue(int amnt, string resource_name) {
        return amnt == 0
            ? ""
            : String.Format("{0} {1} {2}",
                            amnt > 0 ? "gain" : "lose",
                            Math.Abs(amnt),
                            resource_name);
    }
	
	public void ExecuteAction() {
		executeButton.GetComponent<Button>().interactable = false;
		DisableSchedulingUi();
		days -= 1;

		List<string> dialogueList = new List<string>();
		for (int i = 0; i < currentSchedule.Count; i++)
		{	
			string dialogue;
			ActionData action = currentSchedule[i];
			if( money + action.moneyGain >= 0 && influence + action.influenceGain >= 0) 
			{
                influence += action.influenceGain;
                money += action.moneyGain;
                dialogue = String.Format("Did {0} to", action.actionName);

				switch (action.actionType)
				{
				    case Action.ActionType.Farming:
                        string[] resourceName = {"influence", "money", "stress", "charisma"};
                        int[] amnts = {action.influenceGain, action.moneyGain, action.stressGain, action.charismaGain};
                        bool previousResource = false;
                        
                        for (int j = 0; j < 4; j++) {
                            if (amnts[j] == 0)
                                continue;
                            
                            if (previousResource)
                                dialogue += " and";
                            
                            dialogue += " " + MakeGainDialogue(amnts[j], resourceName[j]);
                            previousResource = true;
                        }
                        
                        break;
                        
                    case Action.ActionType.TradingVotes:
                        dialogue += String.Format(" trade {0} influence in order to gain 10 votes",
                                                  Math.Abs(action.influenceGain));
                        forVotesCount += 10;
                        againstVotesCount -= 10;
                        break;
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
            dialogue += ".";
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
            int effectVal = int.Parse(effectWords[1]);
            
			switch (effectWords[0])
			{
				case "inf":
					influence = Math.Max(0, influence + effectVal);
					break;
                    
                case "money":
                    money = Math.Max(0, money + effectVal); 
                    break;
                    
                case "charisma":
                    charisma = Math.Max(0, charisma + effectVal); 
                    break;
                    
                case "stress":
                    stress = Math.Max(0, stress + effectVal); 
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
    
    public void removeTutorial() {
        Destroy(tutorial);   
    }
}
