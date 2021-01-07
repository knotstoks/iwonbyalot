using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confirm : MonoBehaviour
{
    public GameObject SpeechUi,Main;
    // Start is called before the first frame update
    public void Close() {
        Main.GetComponent<Tracker>().SpeechSelectAction();
        SpeechUi.SetActive(false);

    }
}
