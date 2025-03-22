using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerQuest : MonoBehaviour
{
    private bool QuestAccept = false;
    private UIDialogue uiDialogue;
    
    private string IntituleQuete;
    private Dictionary<string, int> touteslesquetes = new Dictionary<string, int>(); 
    private InteractablePNJ lepnj;

    [SerializeField] private GameObject quetePanel; 
    [SerializeField] private GameObject queteEntryPrefab; 

    private void Awake()
    {
        quetePanel.SetActive(false);
        uiDialogue = FindObjectOfType<UIDialogue>();

    }


    protected virtual void Update()
    {
        if (uiDialogue != null)
            {
                uiDialogue.SetInteractablePlayer(this); 
            }
    }
    public void SetInteractablePNJ(InteractablePNJ pnj)
    {
        lepnj = pnj;
    }

    public void lamission(string mission)
    {
        IntituleQuete = mission;
    }

    public void Accepter()
    {
        QuestAccept = true;
        RajouterLaQuete();
    }

    public void Refuser()
    {
        QuestAccept = false;
    }

    private void RajouterLaQuete()
    {
        if (!touteslesquetes.ContainsKey(IntituleQuete))
        {
            touteslesquetes.Add(IntituleQuete, 0); 
        }
    }

    public void UpdateProgression(string quete, int progression)
    {
        if (touteslesquetes.ContainsKey(quete))
        {
            touteslesquetes[quete] = progression; 
        }
    }
    
    public bool Unemissionoupas(string quete)
    {
        return (touteslesquetes.ContainsKey(quete));
    }

    public void Montrellesquetes()
    {
        foreach (Transform child in quetePanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var quete in touteslesquetes)
        {
            GameObject queteEntry = Instantiate(queteEntryPrefab, quetePanel.transform);
            TextMeshProUGUI queteText = queteEntry.GetComponentInChildren<TextMeshProUGUI>();
            queteText.text = $"{quete.Key} : {quete.Value}/4"; 
        }

        quetePanel.SetActive(true);
    }

    public void CacherLesQuetes()
{
    if (quetePanel != null)
    {
        quetePanel.SetActive(false); 
    }
}

public void OnPieceRecuperee()
{
    int nombreprogression = touteslesquetes["Récupérer 4 pièces"];
    UpdateProgression("Récupérer 4 pièces", nombreprogression+1); 
    Montrellesquetes(); 
}
}