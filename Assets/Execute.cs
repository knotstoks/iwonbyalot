using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Execute : MonoBehaviour
{
    public GameObject Track,Slot1,Slot2,Slot3,Slot4;
    public Image im;
    bool ready;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ready = Track.GetComponent<Tracker>().AllDone();
        if(ready){
            im.color = new Color32(0,255,0,100);
        }
        else {
            im.color = new Color32(255,0,0,100);
        }
    }
    public void ExecuteAction(){
        
        if(ready) {
        Track.GetComponent<Tracker>().days -= 1;
        Slot1.GetComponent<TimeSlotter>().Start();
        Slot2.GetComponent<TimeSlotter>().Start();
        Slot3.GetComponent<TimeSlotter>().Start();
        Slot4.GetComponent<TimeSlotter>().Start();
        Track.GetComponent<Tracker>().Refresh();
        
        //run the actions but haven't coded them yet
        }
    }
}
