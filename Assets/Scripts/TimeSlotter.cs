using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlotter : MonoBehaviour
{
    public Text Textfield;
	public Tracker track;
	
	public bool chose;
    public bool choosing;
	bool time_set = false;
    string time;
    string cancel;
    bool otherButtonsPressed;
    string action;

    public void Start() {
        choosing = false;
        chose = false;
        cancel = "cancel";
		
		if (time_set)
			Textfield.text = time;
    }
	
	public void Init(int start_time, Tracker _track) {
		time = start_time.ToString() + ":00 - " + (start_time + 1).ToString() + ":00";
		Textfield.text = time;
		time_set = true;
		track = _track;
	}
	
    void Update() {
        otherButtonsPressed = track.Condition();
        
    }
    public void SetAction()
    {
        if (otherButtonsPressed && !choosing) 
        {

        }  else {
            if (choosing == false ) {
                Textfield.text = cancel;
                choosing = true;
            } else {
                Textfield.text = time;
                choosing = false;
                
            }
        chose = false;
        track.Refresh();
        }
    }
}
