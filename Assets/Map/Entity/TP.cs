using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TP : MonoBehaviour
{

    public HashSet<Collider2D> ignoredArrivals = new();
    [SerializeField] private TP destinationPortal;

    public GameObject pressE;


    void Start()
    {
        if (pressE != null)
        {
            pressE.SetActive(false);
        }

    }

    protected virtual void Update()
    {
    }

    protected void OnTriggerExit2D(Collider2D collider)
    {
        if (pressE != null)
        {
            pressE.SetActive(false);
        }
        ignoredArrivals.Remove(collider);
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (pressE != null)
        {
            pressE.SetActive(true);
        }
    }

    protected void OnTriggerStay2D(Collider2D enteredCollider)
    {
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("portal");
            if (!ignoredArrivals.Contains(enteredCollider)) {
                destinationPortal.ignoredArrivals.Add(enteredCollider);
                destinationPortal.TeleportObject(enteredCollider); }
        }
    }

    public void TeleportObject(Collider2D teleported)
    {
        Debug.Log("tp");
        teleported.transform.position = new Vector3(this.transform.position.x, this.transform.position.y) ;
    }
}
