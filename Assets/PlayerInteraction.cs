using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private List<CollectibleItem> nearbyItems = new List<CollectibleItem>();

    private void Update()
    {
        Debug.Log("dit frr");
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E appuyé");
            TryCollectItem();
        }
    }

    private void TryCollectItem()
    {
        if (nearbyItems.Count > 0)
        {
            CollectibleItem item = nearbyItems[0];
            item.Collect(GetComponent<PlayerInventory>());
            nearbyItems.Remove(item);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CollectibleItem item = other.GetComponent<CollectibleItem>();
        if (item != null)
        {
            nearbyItems.Add(item);
            item.ShowPressE();
            Debug.Log("Objet à proximité : " + other.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CollectibleItem item = other.GetComponent<CollectibleItem>();
        if (item != null)
        {
            nearbyItems.Remove(item);
            item.HidePressE();
            Debug.Log("Objet hors de portée : " + other.name);
        }
    }
}