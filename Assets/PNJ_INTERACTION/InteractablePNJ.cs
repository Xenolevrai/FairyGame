using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractablePNJ : MonoBehaviour
{
    private Collider2D z_Collider;
    [SerializeField] private ContactFilter2D z_Filter;
    private List<Collider2D> z_CollidedObjects = new List<Collider2D>(1);
    public GameObject pressE;
    public List<string> dialogues = new List<string> { "Hello" };
    private UIDialogue uiDialogue;

    protected virtual void Start()
    {
        z_Collider = GetComponent<Collider2D>();
        if (pressE != null)
        {
            pressE.SetActive(false);
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
            ouver();
        }
    }

    protected virtual void OnNotCollided()
    {
        if (pressE != null)
        {
            pressE.SetActive(false);
        }
    }

    public void ouver()
    {
        Debug.Log("ça marche pour le pnj");

        if (uiDialogue != null)
        {
            Debug.Log("oeoeoeoeoeoeo");
            uiDialogue.SetDialogues(dialogues); 
        }
    }
}