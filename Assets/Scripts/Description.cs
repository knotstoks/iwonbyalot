using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    public int days;
    public Text Textfield;
    public string ratio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOriginalText(){
        Textfield.text = "The class is split: " + ratio +"\n\n" + days +" days till election day";
    }
}
