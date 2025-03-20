using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int itemCount = 0;

    public void AddItem(int value)
    {
        itemCount += value;
        Debug.Log("Objet ramassé ! Total : " + itemCount);
    }
}