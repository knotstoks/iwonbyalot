using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confirm : MonoBehaviour
{
    public GameObject SpeechUi,Main,SpeechContainer;
    public bool isSpeech;
    // Start is called before the first frame update
    public void Close() {
        isSpeech = Main.GetComponent<Tracker>().isSpeech;
        if(isSpeech){
        Main.GetComponent<Tracker>().SpeechSelectAction();
        } else {
        Main.GetComponent<Tracker>().ResearchSelectAction();
        }
        SpeechContainer.SetActive(true);
        SpeechUi.SetActive(false);

    }
}
