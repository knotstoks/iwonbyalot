using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using String = System.String;

public class EventExecuter : MonoBehaviour
{
    public Description desc;
    public GameObject choiceSlotPrefab;
    public GameObject choiceTable;

    public delegate void EventFinishCallback(string eventReturn);
    private EventFinishCallback eventFinishCallback;

    private bool isExecutingEvent;
    private int lineIndex;
    private DialogueEvent diaEvent;
    private CurrentStatistics currentStats;
    private int randomDistrictIndex;

    private bool isMakingChoice;
    private List<GameObject> choiceSlots;
    private List<int> choiceIndexes;

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
        choiceIndexes = new List<int>();

        while (diaEvent.dialogueLineTypes[lineIndex] != DialogueEvent.DialogueLineType.ChoiceEnd)
        {
            choices.Add(diaEvent.dialogueLines[lineIndex]);
            choiceIndexes.Add(lineIndex);
            NextLine();
        }

        choiceTable.SetActive(true);
        choiceSlots = new List<GameObject>();
        for (int i = 0; i < choices.Count; i++)
        {
            GameObject choiceSlot = Instantiate(choiceSlotPrefab, choiceTable.transform) as GameObject;
            choiceSlot.GetComponent<ChoiceSlotter>().Init(i, choices[i], this);
            choiceSlots.Add(choiceSlot);
        }
    }

    public void SelectChoice(int index)
    {
        if (!isMakingChoice)
        {
            return;
        }
        isMakingChoice = false;
        for (int i = 0; i < choiceSlots.Count; i++)
        {
            Destroy(choiceSlots[i]);
        }
        choiceTable.SetActive(false);
        lineIndex = diaEvent.dialogueLineLinks[choiceIndexes[index]];
        ExecuteDialogueLine();
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
