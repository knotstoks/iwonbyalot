using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour
{
	public List<GameObject> schedulingUi;
	public GameObject Desc, Timetable, timeslot_prefab, Execute;
	public delegate void TimeslotCallback(Action action);
	private TimeslotCallback timeslotCallback;
	private List<GameObject> timeslots;
	private List<Action> currentSchedule;
	private int selectedTimeslot;
	public int days;
    public float influence;
    public float money;
    
    
    // Start is called before the first frame update
    void Start()
    {
		timeslots = new List<GameObject>();
		currentSchedule = new List<Action>();
		selectedTimeslot = -1;
		Init(12, 16);
    }
	
	void Init(int start_hour, int end_hour) {
		for (int i = start_hour; i < end_hour; i++) {
			GameObject timeslot = Instantiate(timeslot_prefab, Timetable.transform) as GameObject;
			timeslot.GetComponent<TimeSlotter>().Init(i - start_hour, i, this);
			timeslots.Add(timeslot);
			currentSchedule.Add(null);
		}
		
		LayoutRebuilder.ForceRebuildLayoutImmediate(Timetable.GetComponent<RectTransform>());
	}

	public void SelectTimeslot(int index, TimeslotCallback timeslotCallback)
    {
		if (selectedTimeslot != -1)
        {
			this.timeslotCallback(null);

		}
		selectedTimeslot = index;
		this.timeslotCallback = timeslotCallback;
	}

	public void SelectAction(Action action)
	{
		if (selectedTimeslot != -1)
		{
			currentSchedule[selectedTimeslot] = action;
			this.timeslotCallback(action);
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

    public bool AllDone(){
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
			influence += action.getInfluenceGain();
			money += action.getMoneyGain();
			//switch (action.getActionType())
			//{
			//    case Action.ActionType.Farming:

			//}
			string dialogue = String.Format("Did {0} to gain {1} influence and {2} money", 
				action.getName(), action.getInfluenceGain(), action.getMoneyGain());
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
		foreach(GameObject ui in schedulingUi)
        {
			ui.SetActive(false);
        }
    }
}
