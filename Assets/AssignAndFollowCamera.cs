using UnityEngine;

public class AssignAndFollowCamera : MonoBehaviour
{
    public Canvas canvas; // Référence au Canvas
    public string playerCameraTag = "camera"; // Tag de la caméra du joueur
    public Vector3 offset; // Décalage par rapport à la caméra

    private Camera playerCamera;

    void Start()
    {
        // Trouver la caméra du joueur par son tag
        GameObject playerCameraObject = GameObject.FindGameObjectWithTag(playerCameraTag);

        if (playerCameraObject != null)
        {
            // Assigner la caméra au Canvas
            playerCamera = playerCameraObject.GetComponent<Camera>();
            if (canvas != null && playerCamera != null)
            {
                canvas.worldCamera = playerCamera;
                Debug.Log("Caméra du joueur assignée au Canvas.");
            }
            else
            {
                Debug.LogError("Canvas ou caméra non trouvée.");
            }
        }
        else
        {
            Debug.LogError("Caméra du joueur non trouvée avec le tag : " + playerCameraTag);
        }
    }

    void Update()
    {
        if (playerCamera != null)
        {
            // Mettre à jour la position du Canvas pour qu'il suive la caméra
            transform.position = playerCamera.transform.position + offset;
        }
    }
}