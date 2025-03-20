using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Canvas))]
public class UIDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI contentText; 
    private List<string> dialogues = new List<string>(); 
    private int currentDialogueIndex = 0; 
    private bool isTyping = false; 
    private float typingSpeed = 0.05f; 
    private Action onDialogueEnd; // Délégué pour gérer la fin du dialogue

    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        CloseDialogue();
    }

    public void SetDialogues(List<string> dialogueList, Action onEnd)
    {
        dialogues = dialogueList;
        currentDialogueIndex = 0;
        onDialogueEnd = onEnd; // Assigner le délégué
        ShowCurrentDialogue();
    }

    private void ShowCurrentDialogue()
    {
        if (currentDialogueIndex < dialogues.Count)
        {
            canvas.enabled = true;
            StartCoroutine(TypeText(dialogues[currentDialogueIndex])); 
        }
        else
        {
            CloseDialogue();
            onDialogueEnd?.Invoke(); // Appeler le délégué à la fin du dialogue
        }
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        contentText.text = ""; 

        foreach (char letter in text.ToCharArray())
        {
            contentText.text += letter; 
            yield return new WaitForSeconds(typingSpeed); 
        }

        isTyping = false;
    }

    public void NextDialogue()
    {
        if (isTyping) 
        {
            StopAllCoroutines(); 
            contentText.text = dialogues[currentDialogueIndex]; 
            isTyping = false;
        }
        else
        {
            currentDialogueIndex++;
            ShowCurrentDialogue();
        }
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