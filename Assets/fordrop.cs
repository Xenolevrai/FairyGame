using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fordrop : MonoBehaviour, Area
{
  
    public void OnCardDrop(Asset asset)
    {
        asset.transform.position= transform.position;
        Debug.Log("card is here");
    }
}
