using UnityEngine;

public class FollowItemText : MonoBehaviour
{
    public GameObject pressE;
    public Vector3 offset = new Vector3(0, 1, 0);

    private void Update()
    {
        if (pressE != null)
        {
            pressE.transform.position = transform.position + offset;
        }
    }
}