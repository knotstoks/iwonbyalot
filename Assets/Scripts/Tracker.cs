using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour
{
	public List<GameObject> schedulingUi;
	public GameObject Desc, Timetable, ActionTable, actionslotPrefab, timeslotPrefab, Execute;
	private List<GameObject> timeslots;
	private List<GameObject> actionslots;
	private List<Action> currentSchedule;
	private int selectedTimeslot;
	public int days;
    public float influence;
    public float money;
	public List<ActionData> levelOneActionData;


	// Start is called before the first frame update
	void Start()
    {
		timeslots = new List<GameObject>();
		actionslots = new List<GameObject>();
		currentSchedule = new List<Action>();
		selectedTimeslot = -1;
		Init(12, 16);
		Refresh();
	}
	
	void Init(int start_hour, int end_hour) {
		for (int i = start_hour; i < end_hour; i++) {
			GameObject timeslot = Instantiate(timeslotPrefab, Timetable.transform) as GameObject;
			timeslot.GetComponent<TimeSlotter>().Init(i - start_hour, i, this);
			timeslots.Add(timeslot);
			currentSchedule.Add(null);
		}
		for (int i = 0; i < levelOneActionData.Count; i++)
		{
			GameObject actionslot = Instantiate(actionslotPrefab, ActionTable.transform) as GameObject;
			actionslot.GetComponent<ActionSlotter>().Init(levelOneActionData[i], this, Desc);
			actionslots.Add(actionslot);
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(Timetable.GetComponent<RectTransform>());
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
    public void Refresh()
    {
        for (int i = 0; i < timeslots.Count; i++)
        {
			timeslots[i].GetComponent<TimeSlotter>().Refresh();
			currentSchedule[i] = null;
		}

        Desc.GetComponent<Description>().days = days;
        Desc.GetComponent<Description>().SetOriginalText();

        Execute.GetComponent<Button>().interactable = true;
    }

    public bool AllDone() {
        for (int i = 0; i < timeslots.Count; i++) {
			if (currentSchedule[i] == null)
				return false;
		}
		return true;
    }
	
	public void ExecuteAction() {
		if (!AllDone())
        {
			return;
        }
		Execute.GetComponent<Button>().interactable = false;
		DisableSchedulingUi();
		days -= 1;


		List<string> dialogueList = new List<string>();
		for (int i = 0; i < currentSchedule.Count; i++)
		{
			Action action = currentSchedule[i];
			influence += action.influenceGain;
			money += action.moneyGain;
			//switch (action.getActionType())
			//{
			//    case Action.ActionType.Farming:

			//}
			string dialogue = String.Format("Did {0} to gain {1} influence and {2} money", 
				action.actionName, action.influenceGain, action.moneyGain);
			dialogueList.Add(dialogue);
		}
		//run the actions but haven't coded them yet
		Desc.GetComponent<Description>().executeDialogue(dialogueList, ResumeScheduling);
	}

	public void ResumeScheduling()
    {
		foreach (GameObject ui in schedulingUi)
		{
			ui.SetActive(true);
		}
		Refresh();
	}

	private void DisableSchedulingUi()
    {
		foreach (GameObject ui in schedulingUi)
        {
			ui.SetActive(false);
        }
    }
}
