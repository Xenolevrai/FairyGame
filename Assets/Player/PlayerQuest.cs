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
    private List<int> nombreavoir = new List<int>();
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

    public void lamission(string mission, int nombre)
    {
        IntituleQuete = mission;
        nombreavoir.Add(nombre);
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
    int i = 0;
    foreach (var quete in touteslesquetes)
    {
        GameObject queteEntry = Instantiate(queteEntryPrefab, quetePanel.transform);
        TextMeshProUGUI[] textes = queteEntry.GetComponentsInChildren<TextMeshProUGUI>();

        if (textes.Length >= 2)
        {
            textes[0].text = quete.Key;
            textes[0].fontSize = 70; 
            textes[0].color = Color.black; 

            RectTransform intituleRect = textes[0].GetComponent<RectTransform>();
            intituleRect.sizeDelta = new Vector2(1650, intituleRect.sizeDelta.y); 

            textes[1].text = $"{quete.Value}/{nombreavoir[i]}";
            textes[1].fontSize = 70; 
            textes[1].color = Color.black; 

            RectTransform progressionRect = textes[1].GetComponent<RectTransform>();
            progressionRect.sizeDelta = new Vector2(300, progressionRect.sizeDelta.y); 
            i++;
        }
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
}

}