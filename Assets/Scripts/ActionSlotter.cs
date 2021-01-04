using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionSlotter : MonoBehaviour,IPointerExitHandler, IPointerEnterHandler
{   
    public GameObject track;
    public string actionID;
    public bool selected;
     GameObject timeSlot;
    public GameObject bottom;
    public string actionDescription;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        selected = track.GetComponent<Tracker>().Condition();
    }

    public void AssignAction() {
        if(selected){
            timeSlot = track.GetComponent<Tracker>().Chosen();
            timeSlot.GetComponent<TimeSlotter>().Textfield.text = actionID;
            timeSlot.GetComponent<TimeSlotter>().choosing = false; 
            timeSlot.GetComponent<TimeSlotter>().chose = true;
            track.GetComponent<Tracker>().Refresh();
            
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        bottom.GetComponent<Description>().Textfield.text = actionDescription;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
        bottom.GetComponent<Description>().SetOriginalText();
    }
}
