using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeslot : MonoBehaviour
{
	public GameObject time;
	public GameObject activity;
	public Text time_text;
	public Image timeslot_bg;
	
    // Start is called before the first frame update
    void Start()
    {
    }
	
	void Init(int start_time) {
		time_text.text = start_time.ToString() + ":00 - " + (start_time + 1).ToString() + ":00";
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
