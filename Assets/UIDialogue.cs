using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(Canvas))]
public class UIDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private TextMeshProUGUI PersonnagePNJ;
    [SerializeField] private GameObject accepter;
    [SerializeField] private GameObject refuser;

    private List<string> dialogues = new List<string>(); 
    private int currentDialogueIndex = 0; 
    private bool isTyping = false; 
    private float typingSpeed = 0.05f; 
    private Action onDialogueEnd;
    private Canvas canvas;
    private InteractablePNJ Mob;
    private PlayerQuest LaQuete;
    private bool fin = false;
    

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        accepter.SetActive(false);
        refuser.SetActive(false);
        CloseDialogue();
    }

    private void Update()
    {
         if (Input.GetKeyDown(KeyCode.J) && fin)
        {
            InteractQuest();
        }
         if (Input.GetKeyDown(KeyCode.L) && fin)
        {
            InteractNoQuest();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (IsDialogueActive())
            {
                NextDialogue();
            }
        }
    }
    public void SetInteractablePNJ(InteractablePNJ pnj)
    {
        Mob = pnj;
    }
    public void SetInteractablePNJQuete(InteractablePNJ pnj)
    {
        Mob = pnj;
        Mob.SetInteractablePlayer(LaQuete);
    }
    public void SetInteractablePlayer(PlayerQuest player)
    {
        LaQuete = player;
    }
    public void SetDialogues(List<string> dialogueList, Action onEnd )
    {
        accepter.SetActive(false);
        refuser.SetActive(false);
        canvas.enabled = true;
        dialogues = dialogueList;
        currentDialogueIndex = 0;
        onDialogueEnd = onEnd;
        ShowCurrentDialogue();
    }
    public void  SetDialoguesPerso(string Personnage)
    {
        PersonnagePNJ.text = Personnage;
    }
    

    private void ShowCurrentDialogue()
    {
        accepter.SetActive(false);
        refuser.SetActive(false);
        if (currentDialogueIndex < dialogues.Count)
        {
            canvas.enabled = true;
            StartCoroutine(TypeText(dialogues[currentDialogueIndex])); 
        }
        else
        {
            CloseDialogue();
            onDialogueEnd?.Invoke();
        }
    }

    public void Quest(List<string> dialogueList, string Personnage)
    {
        PersonnagePNJ.text = Personnage;
        canvas.enabled = true;
        dialogues = dialogueList;
        currentDialogueIndex = 0;
        ShowCurrentDialogueQuest();
    }
    private void ShowCurrentDialogueQuest()
    {
        if (currentDialogueIndex < dialogues.Count)
        {
            canvas.enabled = true;
            StartCoroutine(TypeText(dialogues[currentDialogueIndex])); 
        }
        else
        {
            fin = true;
            accepter.SetActive(true);
            refuser.SetActive(true);
            canvas.enabled = true;
            StartCoroutine(TypeText(dialogues[dialogues.Count-1])); 
        }
    }

    public void InteractQuest()
    {
        fin = false;
        Mob.questaccepte = true;
        CloseDialogue();
        LaQuete.Accepter();
        Mob.supprimemur();
    }
    public void InteractNoQuest()
    {
        fin = false;
        Mob.questaccepte = false;
        CloseDialogue();
        LaQuete.Refuser();
        Mob.supprimemur();
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
    if (Mob != null && Mob.PNJpourquete) 
    {
        if (isTyping)
        {
            if (currentDialogueIndex < dialogues.Count)
            { 
                StopAllCoroutines();
                contentText.text = dialogues[currentDialogueIndex];
                isTyping = false;
            }
            else
            {
                StopAllCoroutines();
                contentText.text = dialogues[dialogues.Count - 1];
                isTyping = false;
            }
        }
        else
        {
            currentDialogueIndex++;
            ShowCurrentDialogueQuest();
        }
    }
    else
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