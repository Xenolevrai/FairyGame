using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ : InteractablePNJ
{

    private InteractablePNJ pnj;
    


    protected override void Start()
    {
        base.Start();
        pnj = GetComponent<InteractablePNJ>();
    }

    

    protected override void OnCollided(GameObject collidedObject)
    {
        if (pressE != null)
        {
            pressE.SetActive(true);

        }

        if (Input.GetKey(KeyCode.E))
        {
            OnInteract(collidedObject);
        }
    }

    protected virtual void OnInteract(GameObject collidedObject)
    {
        Debug.Log("Interact with PNJ");
        pnj.ouver(collidedObject);

    }

}
