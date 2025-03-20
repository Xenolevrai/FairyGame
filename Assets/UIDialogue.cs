using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Canvas))]
public class UIDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI contentText; 
    private List<string> dialogues = new List<string>(); 
    private int currentDialogueIndex = 0; 

    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        CloseDialogue();
    }

    public void SetDialogues(List<string> dialogueList)
    {
        dialogues = dialogueList;
        currentDialogueIndex = 0;
        ShowCurrentDialogue();
    }

    private void ShowCurrentDialogue()
    {
        if (currentDialogueIndex < dialogues.Count)
        {
            canvas.enabled = true;
            contentText.text = dialogues[currentDialogueIndex];
        }
        else
        {
            CloseDialogue();
        }
    }

    public void NextDialogue()
    {
        currentDialogueIndex++;
        ShowCurrentDialogue();
    }

    public void CloseDialogue()
    {
        canvas.enabled = false;
    }

    public bool IsDialogueActive()
    {
        return canvas.enabled;
    }
}