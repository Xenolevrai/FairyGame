using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : CollidableObject
{
    private bool z_Interacted = false;
    private CollidableObject chest;

    protected override void Start()
    {
        base.Start();
        chest = GetComponent<CollidableObject>();
    }

    protected override void OnCollided(GameObject collidedObject)
    {
        if (pressE != null)
        {
            pressE.SetActive(true);
            Debug.Log("est-ce-que c'est activé ?");

        }

        if (Input.GetKey(KeyCode.E))
        {
            OnInteract();
        }
    }

    protected virtual void OnInteract()
    {
        if (!z_Interacted)
        {
            z_Interacted = true;
            Debug.Log("Interact with chest");
            chest.ouver();
        }
    }
}