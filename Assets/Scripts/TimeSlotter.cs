using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlotter : MonoBehaviour
{
    public bool chose;
    public Text Textfield;
    public bool choosing;
    public string time;
    public string cancel;
    public GameObject track;
    public bool otherButtonsPressed;
    public string action;

    public void Start() {
        choosing = false;
        chose = false;
        cancel = "cancel";
        Textfield.text = time;
    }
    void Update() {
        otherButtonsPressed = track.GetComponent<Tracker>().Condition();
        
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
        track.GetComponent<Tracker>().Refresh();
        

        }
    }
    


}
