using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceSlotter : MonoBehaviour
{
    public Text Textfield;

    private int slotIndex;
    private EventExecuter eventExecuter;

    public void Init(int slotIndex, string text, EventExecuter eventExecuter)
    {
        this.slotIndex = slotIndex;
        Textfield.text = text;
        this.eventExecuter = eventExecuter;
    }

    public void onClick()
    {
        eventExecuter.SelectChoice(slotIndex);
    }
}
