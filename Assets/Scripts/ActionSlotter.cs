using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionSlotter : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public Text Textfield;
    private Tracker track;
    private GameObject bottom;
    private Action representedAction;

    public void Init(ActionData actionData, Tracker track, GameObject bottom)
    {
        representedAction = new Action(actionData);
        this.track = track;
        this.bottom = bottom;
        Textfield.text = representedAction.actionName;
    }

    public void AssignAction()
    {
        track.SelectAction(representedAction);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        bottom.GetComponent<Description>().textfield.text = representedAction.description;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
        bottom.GetComponent<Description>().SetOriginalText();
    }
}
