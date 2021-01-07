using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cancel : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    public void Close(){
        menu.SetActive(false);
    }
}
