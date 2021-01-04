using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    public int days;
    public Text textfield;
    public string ratio;
    public delegate void ResumeGameCallback();
    private ResumeGameCallback resumeGameCallback;
    private bool isExecutingDialogue;
    private List<string> dialogueList;
    private int dialogueProgression;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void executeDialogue(List<string> dialogueList, ResumeGameCallback resumeGameCallback)
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
        this.textfield.text = dialogueList[0];
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
            textfield.text = dialogueList[dialogueProgression];
            dialogueProgression += 1;
        }
    }

    public void SetOriginalText(){
        textfield.text = "The class is split: " + ratio +"\n\n" + days +" days till election day";
    }
}
