using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour
{
	public GameObject Desc, Timetable, timeslot_prefab, Execute;
	private List<GameObject> timeslots;
	private List<bool> timeslots_choosing;
	private List<bool> timeslots_chose;
    public int days;
    public float influence;
    public float money;
    
    
    // Start is called before the first frame update
    void Start()
    {
		timeslots = new List<GameObject>();
		timeslots_choosing = new List<bool>();
		timeslots_chose = new List<bool>();
		Init(12, 16);
    }
	
	void Init(int start_hour, int end_hour) {
		for (int i = start_hour; i < end_hour; i++) {
			GameObject timeslot = Instantiate(timeslot_prefab, Timetable.transform) as GameObject;
			timeslot.GetComponent<TimeSlotter>().Init(i, this);
			timeslots.Add(timeslot);
			timeslots_choosing.Add(false);
			timeslots_chose.Add(false);
		}
		
		LayoutRebuilder.ForceRebuildLayoutImmediate(Timetable.GetComponent<RectTransform>());
	}

    // Update is called once per frame
    public void Refresh()
    {
		for (int i = 0; i < timeslots.Count; i++) {
			timeslots_choosing[i] = timeslots[i].GetComponent<TimeSlotter>().choosing;
			timeslots_chose[i] = timeslots[i].GetComponent<TimeSlotter>().chose;
		}
		
        Desc.GetComponent<Description>().days = days;
        Desc.GetComponent<Description>().SetOriginalText();
		
		if (AllDone()) {
			Execute.GetComponent<Button>().interactable = true;
		}
    }

    public GameObject Chosen() {
        for (int i = 0; i < timeslots.Count; i++) {
			if (timeslots_choosing[i])
				return timeslots[i];
		}
		
		return timeslots[0]; // this should never be reached
    }

    public bool Condition() {
		for (int i = 0; i < timeslots.Count; i++) {
			if (timeslots_choosing[i])
				return true;
		}
        return false;
    }

    public bool AllDone(){
        for (int i = 0; i < timeslots.Count; i++) {
			if (!timeslots_chose[i])
				return false;
		}
		return true;
    }
	
	public void ExecuteAction() {
		days -= 1;
		Execute.GetComponent<Button>().interactable = false;
		
		for (int i = 0; i < timeslots.Count; i++) {
			timeslots[i].GetComponent<TimeSlotter>().Start();
		}
		
		Refresh();
		
		//run the actions but haven't coded them yet
	}
}
