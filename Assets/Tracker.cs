using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public GameObject Slot1,Slot2,Slot3,Slot4,Desc;
    public bool s1,s2,s3,s4;
    public bool c1,c2,c3,c4;
    public int days;
    public float influence;
    public float money;
    
    
    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    public void Refresh()
    {
        s1 = Slot1.GetComponent<TimeSlotter>().choosing;
        s2 = Slot2.GetComponent<TimeSlotter>().choosing;
        s3 = Slot3.GetComponent<TimeSlotter>().choosing;
        s4 = Slot4.GetComponent<TimeSlotter>().choosing;
        c1 = Slot1.GetComponent<TimeSlotter>().chose;
        c2 = Slot2.GetComponent<TimeSlotter>().chose;
        c3 = Slot3.GetComponent<TimeSlotter>().chose;
        c4 = Slot4.GetComponent<TimeSlotter>().chose;
        Desc.GetComponent<Description>().days = days;
        Desc.GetComponent<Description>().SetOriginalText();

    }

    public GameObject Chosen() {
        return s1 ? Slot1 : s2 ? Slot2 : s3 ? Slot3 : Slot4;
    }

    public bool Condition() {
        return s1 || s2 || s3 || s4;
    }

    public bool AllDone(){
        return c1 && c2 && c3 && c4;
    }
}
