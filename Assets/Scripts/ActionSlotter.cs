using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionSlotter : MonoBehaviour
{
    public Text Textfield;
    

    private TimeSlotter timeSlotter;

    void Awake() {
        timeSlotter = GameObject.FindObjectOfType<TimeSlotter> ();
    }

    public void ChooseAction()
    {

    }


}
