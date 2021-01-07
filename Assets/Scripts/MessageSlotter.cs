using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MessageSlotter : MonoBehaviour,IPointerExitHandler, IPointerEnterHandler
{
    public Text Textfield;
    private Tracker track;
    private GameObject bottom;
    public CampaignMessage representedMessage;
    private District curDistrict;
    private int slotIndex; 
    // Start is called before the first frame update
    public void Init(CampaignMessage campaignMessage,Tracker track,GameObject bottom,int index)
    {
        this.track = track;
        this.bottom = bottom;
        representedMessage = campaignMessage;
        Textfield.text = representedMessage.messageName;
        slotIndex = index;
    }

    public void AssignMessage()
    {
        track.SelectMessage(slotIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        bottom.GetComponent<Description>().textfield.text = representedMessage.speech;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
        bottom.GetComponent<Description>().SetOriginalText();
    }
   

}
