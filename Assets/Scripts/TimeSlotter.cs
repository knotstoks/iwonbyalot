using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlotter : MonoBehaviour
{
    public Text Textfield;
	public Tracker track;
	
	//public bool chose;
    public bool isTimeslotSelected;
	bool time_set = false;
    string time;
    //bool otherButtonsPressed;
    //string action;
    private ActionData curAction;
    private int slotIndex;

    public void Start() {
        isTimeslotSelected = false;
        //chose = false;
		
		if (time_set)
			Textfield.text = time;
    }

    public void Init(int slotIndex, int start_time, Tracker track) {
		time = start_time.ToString() + ":00 - " + (start_time + 1).ToString() + ":00";
		Textfield.text = time;
		time_set = true;
        this.slotIndex = slotIndex;
		this.track = track;
	}

    public void Reset()
    {
        Textfield.text = time;
        isTimeslotSelected = false;
    }

    public void onClick()
    {
        if (isTimeslotSelected)
        {
            Textfield.text = curAction == null ? time : curAction.actionName;
            isTimeslotSelected = false;
        }
        else
        {
            track.SelectTimeslot(slotIndex);
            Textfield.text = "Cancel";
            isTimeslotSelected = true;
        }
    }

    public void SetAction(ActionData action)
    {
        isTimeslotSelected = false;
        if (action == null)
        {
            Textfield.text = curAction == null ? time : curAction.actionName;
            return;
        } 
        curAction = action;
        Textfield.text = curAction.actionName;
        track.UpdateExecute();
    }
}
