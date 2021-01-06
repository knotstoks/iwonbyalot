using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using String = System.String;

public class EventExecuter : MonoBehaviour
{
    public Description desc;

    public delegate void EventFinishCallback(string eventReturn);
    private EventFinishCallback eventFinishCallback;

    private bool isExecutingEvent;
    private bool isMakingChoice;
    private int lineIndex;
    private DialogueEvent diaEvent;
    private CurrentStatistics currentStats;
    private int randomDistrictIndex;


    public void ExecuteEventDialogue(DialogueEvent dialogueEvent,
        CurrentStatistics currentStats,
        EventFinishCallback eventFinishCallback)
    {
        isExecutingEvent = true;
        isMakingChoice = false;
        this.diaEvent = dialogueEvent;
        this.currentStats = currentStats;
        this.eventFinishCallback = eventFinishCallback;
        randomDistrictIndex = -1;
        lineIndex = 0;
        ExecuteDialogueLine();
        gameObject.GetComponent<Button>().interactable = true;
    }

    public void ExecuteDialogueLine()
    {
        switch(diaEvent.dialogueLineTypes[lineIndex])
        {
            case DialogueEvent.DialogueLineType.Text:
                SayLine();
                NextLine();
                break;
            case DialogueEvent.DialogueLineType.RandomDistrict:
                randomDistrictIndex = Random.Range(0, currentStats.districts.Count);
                desc.Say(
                    String.Format(
                        diaEvent.dialogueLines[lineIndex],
                        currentStats.districts[randomDistrictIndex].name
                ));
                NextLine();
                break;
            case DialogueEvent.DialogueLineType.Jump:
                NextLine();
                break;
            case DialogueEvent.DialogueLineType.StartChoice:
                ParseChoice();
                break;
            case DialogueEvent.DialogueLineType.Return:
                EndEvent();
                break;
        }
    }

    public void OnClick()
    {
        if (isExecutingEvent && !isMakingChoice)
        {
            ExecuteDialogueLine();
        }
    }

    private void NextLine()
    {
        lineIndex = diaEvent.dialogueLineLinks[lineIndex];
    }

    private void SayLine()
    {
        desc.Say(diaEvent.dialogueLines[lineIndex]);
    }

    public void ParseChoice()
    {
        isMakingChoice = true;
        SayLine();
        NextLine();
        List<string> choices = new List<String>();
        while(diaEvent.dialogueLineTypes[lineIndex] != DialogueEvent.DialogueLineType.ChoiceEnd)
        {
            choices.Add(diaEvent.dialogueLines[lineIndex]);
            NextLine();
        }
    }

    public void EndEvent()
    {
        isExecutingEvent = false;
        gameObject.GetComponent<Button>().interactable = false;
        string result = diaEvent.dialogueLines[lineIndex];
        if (randomDistrictIndex != -1)
        {
            result = String.Format(result, randomDistrictIndex);
        }
        eventFinishCallback(result);
    }


}
