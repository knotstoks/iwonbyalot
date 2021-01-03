using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlotter : MonoBehaviour
{
    public Text Textfield;
    public bool choosing = false;
    public string previous;

    public void SetAction(string text)
    {
        if (choosing == false) {
            previous = Textfield.text;
            Textfield.text = text;
            choosing = true;
        } else {
            Textfield.text = previous;
            choosing = false;
        }
    }


}
