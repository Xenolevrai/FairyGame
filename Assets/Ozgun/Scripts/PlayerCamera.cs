using Mirror;
using UnityEngine;
using Cinemachine;

public class PlayerCamera : NetworkBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera; // Référence à la CinemachineVirtualCamera

    void Start()
    {
        // Seul le client local configure sa propre caméra
        if (isLocalPlayer)
        {
            // Trouve la caméra virtuelle dans les enfants du joueur (si elle n'est pas assignée manuellement)
            if (virtualCamera == null)
            {
                virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
                if (virtualCamera == null)
                {
                    Debug.LogError("CinemachineVirtualCamera not found in player's children!");
                }
                else
                {
                    Debug.Log("CinemachineVirtualCamera found and assigned!");
                }
            }

            // Active la caméra virtuelle
            if (virtualCamera != null)
            {
                virtualCamera.gameObject.SetActive(true);

                // Fait suivre le joueur par la caméra
                virtualCamera.Follow = transform; // Suit le transform du joueur
                virtualCamera.LookAt = transform; // Regarde le transform du joueur (optionnel)
            }
            else
            {
                Debug.LogError("CinemachineVirtualCamera is not assigned!");
            }
        }
        else
        {
            // Désactive la caméra pour les autres joueurs
            if (virtualCamera != null)
            {
                virtualCamera.gameObject.SetActive(false);
            }
        }
    }
}