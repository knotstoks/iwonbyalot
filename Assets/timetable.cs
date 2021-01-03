using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timetable : MonoBehaviour
{
	public GameObject timeslot;
	
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 8; i <= 16; i++) {
			GameObject new_timeslot = Instantiate(timeslot, this.transform) as GameObject;
			new_timeslot.SendMessage("Init", i);
		}
		
		Debug.Log("ff");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
