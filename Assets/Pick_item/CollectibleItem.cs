using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public int value = 1;
    public GameObject pressE; 

    private void Start()
    {
        if (pressE != null)
        {
            pressE.SetActive(false); 
        }
    }

    public void Collect(PlayerInventory inventory)
    {
        if (inventory != null)
        {
            inventory.AddItem(value);
            Debug.Log("Objet ramassé !");
            Destroy(gameObject);
        }
    }

    public void ShowPressE()
    {
        if (pressE != null)
        {
            pressE.SetActive(true); 
        }
    }

    public void HidePressE()
    {
        if (pressE != null)
        {
            pressE.SetActive(false);
        }
    }
}