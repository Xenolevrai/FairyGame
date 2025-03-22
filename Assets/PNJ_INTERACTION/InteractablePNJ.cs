using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractablePNJ : MonoBehaviour
{
    public bool questaccepte = false;
    public bool finiquest = false;
    private Collider2D z_Collider;
    [SerializeField] private ContactFilter2D z_Filter;
    private List<Collider2D> z_CollidedObjects = new List<Collider2D>(1);
    public GameObject pressE;
    public List<string> dialogues = new List<string> { "Hello" };
    private UIDialogue uiDialogue;
    private PlayerQuest lejoueur;
    public GameObject Mur;
    private MovePlayer playerMovement;
    
    public bool detruire = false;
    public bool pouvoirparlerplusieursfois = false;
    private bool dejaparle = false;
    public bool PNJpourquete = false;
    public string Personnage;

    public string Lamissiondonne;
    public int quetenombre;
  
    


    protected virtual void Start()
    {
        z_Collider = GetComponent<Collider2D>();
        if (pressE != null)
        {
            pressE.SetActive(false);
        }
        if (Mur != null)
        {
            Mur.SetActive(true);
        }

    }

    protected virtual void Update()
    {
        z_Collider.OverlapCollider(z_Filter, z_CollidedObjects);

        if (z_CollidedObjects.Count > 0)
        {
            OnCollided(z_CollidedObjects[0].gameObject);
        }
        else
        {
            OnNotCollided();
        }
    }

    private void Awake()
    {
        uiDialogue = FindObjectOfType<UIDialogue>();
    }

    protected virtual void OnCollided(GameObject collidedObject)
    {
        if (pressE != null)
        {
            pressE.SetActive(true);
            Debug.Log("est-ce-que c'est activé ?");
        }

        Debug.Log("frrrr t'abuses ?");
        if (Input.GetKeyDown(KeyCode.E))
        {
            ouver(collidedObject); 
        }
    }

    public void SetInteractablePlayer(PlayerQuest player)
    {
        lejoueur = player;
        player.SetInteractablePNJ(this);
        player.lamission(Lamissiondonne, quetenombre);
    }
    protected virtual void OnNotCollided()
    {
        if (pressE != null)
        {
            pressE.SetActive(false);
        }
    }

    public void ouver(GameObject collidedObject)
{
    Debug.Log("ça marche pour le pnj");
    if (pouvoirparlerplusieursfois)
    {
        if (PNJpourquete)
        {
            playerMovement = collidedObject.GetComponent<MovePlayer>();
            if (playerMovement != null)
            {   
                playerMovement.SetCanMove(false); 
            }
            if (uiDialogue != null)
            {
                uiDialogue.SetInteractablePNJQuete(this); 
                uiDialogue.Quest(dialogues, Personnage);
            }
            dejaparle = true;
        }
        else
        {
            playerMovement = collidedObject.GetComponent<MovePlayer>();
            if (playerMovement != null)
            {   
                playerMovement.SetCanMove(false); 
            }

            if (uiDialogue != null)
            {
                Debug.Log("oeoeoeoeoeoeo");
                uiDialogue.SetInteractablePNJ(this);
                uiDialogue.SetDialoguesPerso(Personnage);
                uiDialogue.SetDialogues(dialogues, () =>
                {
                    supprimemur();
                });
            }
            dejaparle = true;
        }
    }
    else 
    {
        if (dejaparle)
        {
        }
        else
        {
             if (PNJpourquete)
        {
            playerMovement = collidedObject.GetComponent<MovePlayer>();
            if (playerMovement != null)
            {   
                playerMovement.SetCanMove(false); 
            }
            if (uiDialogue != null)
            {
                uiDialogue.SetInteractablePNJQuete(this); 
                uiDialogue.Quest(dialogues, Personnage);
            }
            dejaparle = true;
        }
        else
        {
            playerMovement = collidedObject.GetComponent<MovePlayer>();
            if (playerMovement != null)
            {   
                playerMovement.SetCanMove(false); 
            }

            if (uiDialogue != null)
            {
                Debug.Log("oeoeoeoeoeoeo");
                uiDialogue.SetInteractablePNJ(this);
                uiDialogue.SetDialoguesPerso(Personnage);
                uiDialogue.SetDialogues(dialogues, () =>
                {
                    supprimemur();
                });
            }
            dejaparle = true;
        }
        }
    }
}

    public void supprimemur()
    {
        if (Mur != null)
        {
            Mur.SetActive(false);
        }

        if (playerMovement != null)
        {
            playerMovement.SetCanMove(true); 
        }

        if (detruire)
        {
            gameObject.SetActive(false); 
        }
    }
}