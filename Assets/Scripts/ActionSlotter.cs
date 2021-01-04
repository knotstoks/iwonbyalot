using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionSlotter : MonoBehaviour,IPointerExitHandler, IPointerEnterHandler
{   
    public GameObject track;
    public string actionID;
    public bool selected;
    public GameObject bottom;
    public string actionDescription;
    private Action representedAction;

    // Start is called before the first frame update
    void Start()
    {
        representedAction = new Action(actionID, actionDescription, 0, 0, Action.ActionType.Farming);
    }

    public void AssignAction()
    {
        track.GetComponent<Tracker>().SelectAction(representedAction);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        bottom.GetComponent<Description>().Textfield.text = actionDescription;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
        bottom.GetComponent<Description>().SetOriginalText();
    }
}
