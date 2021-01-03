using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlotter : MonoBehaviour
{
    public bool test;

    // Start is called before the first frame update
    void Start()
    {
        test = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (test == true) 
        {
            Debug.Log("hello!");
        }
    }

}
