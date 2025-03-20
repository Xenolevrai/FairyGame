using UnityEngine;

public class AssignAndFollowCamera : MonoBehaviour
{
    public Canvas canvas; // R�f�rence au Canvas
    public string playerCameraTag = "camera"; // Tag de la cam�ra du joueur
    public Vector3 offset; // D�calage par rapport � la cam�ra

    private Camera playerCamera;

    void Start()
    {
        // Trouver la cam�ra du joueur par son tag
        GameObject playerCameraObject = GameObject.FindGameObjectWithTag(playerCameraTag);

        if (playerCameraObject != null)
        {
            // Assigner la cam�ra au Canvas
            playerCamera = playerCameraObject.GetComponent<Camera>();
            if (canvas != null && playerCamera != null)
            {
                canvas.worldCamera = playerCamera;
                Debug.Log("Cam�ra du joueur assign�e au Canvas.");
            }
            else
            {
                Debug.LogError("Canvas ou cam�ra non trouv�e.");
            }
        }
        else
        {
            Debug.LogError("Cam�ra du joueur non trouv�e avec le tag : " + playerCameraTag);
        }
    }

    void Update()
    {
        if (playerCamera != null)
        {
            // Mettre � jour la position du Canvas pour qu'il suive la cam�ra
            transform.position = playerCamera.transform.position + offset;
        }
    }
}