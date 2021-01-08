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
	public GameObject Desc, actionslotPrefab, timeslotPrefab, executeButton, messageslotPrefab, 
		TimeslotContainer, ActionContainer, ResourceContainer,MapUi, SpeechUi,SpeechContainer, Confirm;
    private GameObject InfluenceSlider, MoneySlider, StressSlider, CharismaSlider, tutorial,
		miniMap, bigMap;
	public List<GameObject> schedulingUi;

	private List<CampaignMessage> campaignMessages;
	private List<RandomDialogueEvent> randomDialogueEvents;
    private List<ActionData> actions;
	private List<GameObject> resourceslots;
	private List<GameObject> timeslots;
	private List<GameObject> actionslots;
	public List<GameObject> messageslots;
    
    public List<GameObject> tutorialPrefabs;

	public List<ActionData> currentSchedule;
	public ActionData speech;
	public List<int> actionDistrictTarget;
	private List<int> actionMessageTarget;

	public bool isUsingDistricts;
	private List<District> districts;
	private GameObject mapContainer;

	public float eventChance = 0.5f;
	public int likeVoteGain, neutralVoteGain, dislikeVoteGain;
    public float money, influence, stress, charisma;
	public int forVotesCount, totalVotesCount;
	private int selectedTimeslot;
	private int days;

	private string defaultBackground;
    
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
		campaignMessages = new List<CampaignMessage>();

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
		campaignMessages = levelData.campaignMessages;
		resourceslots = levelData.resources;
        forVotesCount = levelData.forVotesStart;
        totalVotesCount = levelData.totalVotes;
		defaultBackground = levelData.defaultBackground;

		int districtCount = levelData.districtNames.Count;
		isUsingDistricts = levelData.districtNames.Count > 0;
		if (isUsingDistricts)
        {
			mapContainer = levelData.mapContainer;
			schedulingUi.Add(MapUi);
			districts = new List<District>();
			List<int> msgIndexes = Enumerable.Range(0, levelData.campaignMessages.Count).ToList();
			for (int i = 0; i < districtCount; i++)
			{
				districts.Add(new District(msgIndexes, 1, 1));
				districts[i].name = levelData.districtNames[i];
				districts[i].forVotesCount = forVotesCount;
				districts[i].totalVotesCount = totalVotesCount;
			}
			forVotesCount *= districtCount;
			totalVotesCount *= districtCount;
		}
        Init(levelData.startTime, levelData.endTime);
		
		if (levelData.introductionDialogue != null)
        {
			DisableSchedulingUi();
			Desc.GetComponent<EventExecuter>().ExecuteEventDialogue(
						levelData.introductionDialogue,
						makeCurrentStatistics(),
						ResumeSchedulingRedirect
						);
        } else {
			Reset();
		}
        
        if (DataPassedToMainGame.tutorial) {
			tutorial = Instantiate(tutorialPrefabs[levelData.level - 1], this.transform);
            switch (levelData.level) {
                case 1:
                    tutorial.GetComponent<Tutorial>().Init(levelData.level, this);
                    break;
                case 2:
                    tutorial.GetComponent<Tutorial>().Init(levelData.level, this, MapUi.GetComponentInChildren<MapButton>(), SpeechUi);
                    break;
            }
		} 
	}

	void Init(int start_hour, int end_hour) {
		selectedTimeslot = -1;
		for (int i = start_hour; i < end_hour; i++) {
			GameObject timeslot = Instantiate(timeslotPrefab, TimeslotContainer.transform);
			timeslot.GetComponent<TimeSlotter>().Init(i - start_hour, i, this);
			timeslots.Add(timeslot);
			currentSchedule.Add(null);
			actionDistrictTarget.Add(-1);
			actionMessageTarget.Add(-1);
		}
        
		for (int i = 0; i < actions.Count; i++)
		{
			GameObject actionslot = Instantiate(actionslotPrefab, ActionContainer.transform);
			actionslot.GetComponent<ActionSlotter>().Init(actions[i], this, Desc);
			actionslots.Add(actionslot);
		}

		for(int i = 0; i < resourceslots.Count; i++)
		{
			Instantiate(resourceslots[i],ResourceContainer.transform);
		}
        MoneySlider = GameObject.FindWithTag("Money");
		InfluenceSlider = GameObject.FindWithTag("Influence");
		if(levelData.advancedResourcesEnabled) {
		StressSlider = GameObject.FindWithTag("Stress");
		CharismaSlider = GameObject.FindWithTag("Charisma");
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(ActionContainer.GetComponent<RectTransform>());
        
		if(isUsingDistricts)
		{
			miniMap = Instantiate(mapContainer, MapUi.transform.Find("Button"));
			bigMap = Instantiate(mapContainer, MapUi.transform.Find("Map Container"));
			miniMap.GetComponent<MapController>().setMini();
			miniMap.GetComponent<MapController>().UpdateDistricts(districts);
			bigMap.GetComponent<MapController>().UpdateDistricts(districts);
			MapUi.SetActive(true);
			MapUi.GetComponentInChildren<MapButton>().Init(bigMap);
			SpeechUi.SetActive(true);
			for (int i = 0; i < campaignMessages.Count; i++ )
			{
				GameObject messageslot = Instantiate(messageslotPrefab,SpeechContainer.transform) as GameObject;
				
				messageslot.GetComponent<MessageSlotter>().Init(campaignMessages[i],this,Desc,i);
				messageslots.Add(messageslot);
				
			}
			GameObject speechMap = Instantiate(mapContainer,SpeechUi.transform.Find("SpeechMap"));
			speechMap.GetComponent<MapController>().DisableDistricts(this);
			speechMap.GetComponent<MapController>().Buttonify(this);
			SpeechUi.SetActive(false);
			
		}
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
			if (action.actionType == ActionData.ActionType.TradingVotes && levelData.messageEnabled){
				SpeechUi.SetActive(true);
				Confirm.GetComponent<Button>().interactable = false;
			} else
			{
                currentSchedule[selectedTimeslot] = action;
                timeslots[selectedTimeslot].GetComponent<TimeSlotter>().SetAction(action);
                selectedTimeslot = -1;
			}
		}
	}

	public void SpeechSelectAction()
	{
		currentSchedule[selectedTimeslot] = speech ;
		timeslots[selectedTimeslot].GetComponent<TimeSlotter>().SetAction(speech);
		selectedTimeslot = -1;
	}

	public void SelectDistrict(int districtIndex) 
	{
		actionDistrictTarget[selectedTimeslot] = districtIndex;
		if(actionMessageTarget[selectedTimeslot] != -1)
		{
			Confirm.GetComponent<Button>().interactable = true;
		}
	}

	public void SelectMessage(int messageIndex)
	{
		actionMessageTarget[selectedTimeslot] = messageIndex;
		if(actionDistrictTarget[selectedTimeslot] != -1)
		{
			Confirm.GetComponent<Button>().interactable = true;
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
		Desc.GetComponent<Description>().ratio = String.Format("{0} / {1}",forVotesCount, totalVotesCount - forVotesCount);
		Desc.GetComponent<Description>().SetOriginalText();
		InfluenceSlider.GetComponentInChildren<Text>().text = influence.ToString() + " /100";
		InfluenceSlider.GetComponent<Slider>().value = influence;
		MoneySlider.GetComponentInChildren<Text>().text = money.ToString() + "/ 100";
		MoneySlider.GetComponent<Slider>().value = money;
		if(levelData.advancedResourcesEnabled){
		StressSlider.GetComponentInChildren<Text>().text = stress.ToString() + "/ 100";
		StressSlider.GetComponent<Slider>().value = stress;
		CharismaSlider.GetComponentInChildren<Text>().text = charisma.ToString() + "/ 100";
		CharismaSlider.GetComponent<Slider>().value = charisma;
		}

		if (isUsingDistricts)
        {
			miniMap.GetComponent<MapController>().UpdateDistricts(districts);
			bigMap.GetComponent<MapController>().UpdateDistricts(districts);
        }
		
        executeButton.GetComponent<Button>().interactable = false;
		Desc.GetComponent<EventExecuter>().LoadBackground(defaultBackground);
		if(days == 0){
			SceneManager.LoadScene(0);
		}
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

	private string ResearchDisliked(District district)
    {
		int foundMsg = district.dislikedMsgs[district.dislikedMsgsFound];
		district.dislikedMsgsFound += 1;
		return String.Format(" You found that {0} did not like it if {1} was implemented",
			district.name, campaignMessages[foundMsg]);
	}

	private string ResearchNeutral(District district)
	{
		int foundMsg = district.neutralMsgs[district.neutralMsgsFound];
		district.neutralMsgsFound += 1;
		return String.Format(" You found that {0} did not care if {1} was implemented",
			district.name, campaignMessages[foundMsg]);
	}

	public void ExecuteAction() {
		executeButton.GetComponent<Button>().interactable = false;
		DisableSchedulingUi();
		days -= 1;

		List<string> dialogueList = new List<string>();
		for (int i = 0; i < currentSchedule.Count; i++)
		{	print(actionDistrictTarget[i]);
			string dialogue;
			ActionData action = currentSchedule[i];
			if( money + action.moneyGain >= 0 && influence + action.influenceGain >= 0 && stress + action.stressGain < 100 && charisma + action.charismaGain >= 0) 
			{
				
                influence += action.influenceGain;
                money += action.moneyGain;
				if(levelData.advancedResourcesEnabled) {
				stress += action.stressGain;
				charisma += action.charismaGain;
				}
                dialogue = String.Format("Did {0} to", action.actionName);
				

				switch (action.actionType)
				{
				    case ActionData.ActionType.Farming:
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
                        
                    case ActionData.ActionType.TradingVotes:
						if (isUsingDistricts)
                        {
                            District selectedDistrict = districts[actionDistrictTarget[i]];
                            int selectedMsg = actionMessageTarget[i];
                            if (selectedDistrict.likedMsgs.Contains(selectedMsg))
                            {
                                dialogue = campaignMessages[selectedMsg].like;
								selectedDistrict.forVotesCount += likeVoteGain;
								forVotesCount += likeVoteGain;
							}
                            else if (selectedDistrict.dislikedMsgs.Contains(selectedMsg))
                            {
                                dialogue = campaignMessages[selectedMsg].dislike;
								if(selectedDistrict.forVotesCount + dislikeVoteGain >= 0) {
								selectedDistrict.forVotesCount += dislikeVoteGain;
								forVotesCount += dislikeVoteGain;
								} 
								else {
									forVotesCount -= selectedDistrict.forVotesCount;
									selectedDistrict.forVotesCount = 0;
								}
								
							}
                            else
                            {
                                dialogue = campaignMessages[selectedMsg].neutral;
								selectedDistrict.forVotesCount += neutralVoteGain;
								forVotesCount += neutralVoteGain;
							}
							actionDistrictTarget[i] = -1;
							actionMessageTarget[i] = -1;
                        }
                        else
                        {
							dialogue += String.Format(" trade {0} influence in order to gain 10 votes",
														Math.Abs(action.influenceGain));
							forVotesCount += 10;
                        }
						break;

					case ActionData.ActionType.Research:
                        {
							
							District district = districts[actionDistrictTarget[i]];
							if (district.neutralMsgsFound == district.neutralMsgs.Count)
                            {
								if (district.dislikedMsgsFound == district.dislikedMsgs.Count)
                                {
									dialogue = "It seems like you already know this districts tastes.";
								}
								else
                                {
									dialogue = ResearchDisliked(district);
								}
                            }
							else if (district.dislikedMsgsFound == district.dislikedMsgs.Count)
                            {
								dialogue = ResearchNeutral(district);
							}
							else if (Random.Range(0.0f, 1.0f) > 0.8)
							{
								// is dislike
								dialogue = ResearchDisliked(district);
							}
							else
							{
								dialogue = ResearchNeutral(district);
							}
							break;
                        }
				}
			}
			else if (stress + action.stressGain >= 100) {
				dialogue = String.Format("...");
				dialogueList.Add(dialogue);
				dialogueList.Add("Where is this place...");
				dialogueList.Add("Oh... It's my room");
				dialogueList.Add("(You exit your room and see your mom)");
				dialogueList.Add("(You feel sluggish)");
				dialogueList.Add("Mom: Oh dear, you're awake, you've been out for two days!");
				dialogueList.Add("Two days?");
				dialogueList.Add("Feeling tired, you drag yourself out of house");
				i = currentSchedule.Count;
				stress = 0;
				days -= 1;
				CalculateVoteChange();

			} else 
			{
				dialogue = String.Format("You do not have enough resources to do {0}",action.actionName);
			}
            dialogue += ".";
			dialogueList.Add(dialogue);
		}
		if(isUsingDistricts)
		{
			CalculateVoteChange();
		}		 
		else 
		{
			if  (forVotesCount - levelData.opponentGainPerRound < 0)
			{
				forVotesCount = 0;
			}	 
			else 
			{
				forVotesCount -= levelData.opponentGainPerRound;
			}
		}
		

		if (days <= 0) 
		{
		
			if(forVotesCount >= totalVotesCount / 2) 
			{
				dialogueList.Add(String.Format(" Congratulations! You have won the election with a result of {0} against {1}", 
					forVotesCount, totalVotesCount - forVotesCount)); 
				dialogueList.Add("(On your social account......)");
				dialogueList.Add("'Guys!");
				dialogueList.Add("I won this election, by a lot!'");


			}
			else 
			{
				dialogueList.Add(String.Format("You have lost the election with a result of {0} against {1}.",
					forVotesCount, totalVotesCount - forVotesCount));
				dialogueList.Add("(On your social account....)");
				dialogueList.Add("'Guys!");
				dialogueList.Add("I won this election, by a lot!'");
				dialogueList.Add("Wait...");
				dialogueList.Add("I lost? That can't be!");
				dialogueList.Add("This is FRAUD!");
					
					
			}
			
			
			//NextLevel();
		}
		Desc.GetComponent<Description>().ExecuteActionsDialogue(dialogueList, ResumeScheduling);
	}

	public void CalculateVoteChange()
	{
		int votesSum = 0;
			for(int i = 0; i < districts.Count;i++)
			{
				if( districts[i].forVotesCount - levelData.opponentGainPerRound >= 0) 
				{
					districts[i].forVotesCount -= levelData.opponentGainPerRound;
				}
				 else
				{
					districts[i].forVotesCount = 0;
				}
				votesSum += districts[i].forVotesCount;
			}
			forVotesCount = votesSum;
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
		// basically the return string will be something like:
		// "inf 10, money 10" which means influence +=10, money += 10
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

				case "district":
					int districtIndex = int.Parse(effectWords[2]);
					
					districts[districtIndex].forVotesCount += effectVal;
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
    
    public void ResumeSchedulingRedirect(string result) { // used for EventCallBack
        ResumeScheduling();
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
