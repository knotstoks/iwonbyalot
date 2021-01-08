using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cancel : MonoBehaviour
{
    public GameObject menu,speechContainer,track;
    // Start is called before the first frame update
    public void Close(){
        
        speechContainer.SetActive(true);
        menu.SetActive(false);
        track.GetComponent<Tracker>().reAssignDistrictAndMessage();
    }
}
