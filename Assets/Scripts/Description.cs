using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    public int days;
    public string ratio;

    public Text textfield;
    public delegate void ResumeGameCallback();
    private ResumeGameCallback resumeGameCallback;
    private bool isExecutingDialogue;
    private List<string> dialogueList;
    private int dialogueProgression;

    private Coroutine speaking = null;
    private string targetSpeech = "";
    [HideInInspector] public bool isWaitingForUserInput = false;

    /// <summary>
    /// Say something and show it on the speech box.
    /// </summary>
    public void Say(string speech)
    {
        StopSpeaking();

        speaking = StartCoroutine(Speaking(speech));
    }

    public void StopSpeaking()
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
        }
        speaking = null;
    }

    public bool isSpeaking { get { return speaking != null; } }
    
    IEnumerator Speaking(string speech)
    {
        targetSpeech = speech;

        textfield.text = "";

        isWaitingForUserInput = false;

        while (textfield.text != targetSpeech)
        {
            textfield.text += targetSpeech[textfield.text.Length];
            yield return new WaitForEndOfFrame();
        }

        //text finished
        isWaitingForUserInput = true;
        while (isWaitingForUserInput)
            yield return new WaitForEndOfFrame();

        StopSpeaking();
    }

    public void ExecuteActionsDialogue(List<string> dialogueList, ResumeGameCallback resumeGameCallback)
    {
        if (dialogueList.Count == 0)
        {
            resumeGameCallback();
            return;
        }
        this.isExecutingDialogue = true;
        this.dialogueProgression = 1;
        this.dialogueList = dialogueList;
        this.resumeGameCallback = resumeGameCallback;
        Say(dialogueList[0]);
        gameObject.GetComponent<Button>().interactable = true;
    }

    public void OnClick()
    {
        if (isExecutingDialogue)
        {
            if (dialogueProgression == dialogueList.Count)
            {
                isExecutingDialogue = false;
                resumeGameCallback();
                gameObject.GetComponent<Button>().interactable = false;
                return;
            }
            Say(dialogueList[dialogueProgression]);
            dialogueProgression += 1;
        }
    }

    public void SetOriginalText(){
        textfield.text = "The class is split: " + ratio +"\n\n" + days +" days till election day";
    }
}
