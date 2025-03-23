using Mirror;
using UnityEngine;

public class MovePlayer : NetworkBehaviour
{
    [SerializeField] private float speed = 3f; // Vitesse de déplacement
    private Rigidbody2D rb; // Référence au Rigidbody2D
    private Animator animator; // Référence à l'Animator

    private Vector2 movement; // Direction du mouvement
    public bool canMove = true; // Autorisation de mouvement

    [SerializeField] private Transform spriteChild; // Référence à l'enfant du sprite (PourAnimation)

    // Synchronisation de l'état de marche
    [SyncVar(hook = nameof(OnAnimationStateChanged))]
    private bool isWalking = false;

    void Start()
    {
        // Initialisation des composants
        rb = GetComponent<Rigidbody2D>();

        // Récupérer l'Animator de l'enfant PourAnimation
        if (spriteChild != null)
        {
            animator = spriteChild.GetComponent<Animator>();
        }

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D non trouvé sur l'objet.");
        }
        if (animator == null)
        {
            Debug.LogError("Animator non trouvé sur l'objet PourAnimation.");
        }
        if (spriteChild == null)
        {
            Debug.LogError("SpriteChild non assigné. Assurez-vous d'assigner l'enfant du sprite.");
        }
    }

    void Update()
    {
        // Seul le joueur local peut contrôler le mouvement
        if (!isLocalPlayer || !canMove) return;

        // Récupération des inputs
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalisation du vecteur de mouvement pour éviter les vitesses diagonales plus rapides
        movement = movement.normalized;

        // Mise à jour de l'état de marche et déclenchement des commandes
        if (movement != Vector2.zero)
        {
            CmdMove(movement);
            CmdSetWalking(true); // Déclencher l'animation de marche

            // Retourner le sprite du joueur s'il va vers la gauche
            if (movement.x != 0 && spriteChild != null)
            {
                Vector3 newScale = spriteChild.localScale;
                newScale.x = Mathf.Abs(newScale.x) * (movement.x < 0 ? -1 : 1); // Inverser l'échelle X si nécessaire
                spriteChild.localScale = newScale;
            }
        }
        else
        {
            CmdStop();
            CmdSetWalking(false); // Arrêter l'animation de marche
        }

        // Mise à jour de l'Animator localement (pour une réactivité immédiate)
        if (animator != null)
        {
            animator.SetFloat("Speed", movement.magnitude * speed);
        }
    }

    private void FixedUpdate()
    {
        // Appliquer le mouvement via Translate
        if (isLocalPlayer && canMove && movement != Vector2.zero)
        {
            var moveAmount = movement * speed * Time.fixedDeltaTime;
            this.transform.Translate(new Vector3(moveAmount.x, moveAmount.y), Space.World);
        }
    }

    [Command]
    private void CmdMove(Vector2 direction)
    {
        // Synchroniser le mouvement sur le serveur
        rb.velocity = direction * speed;
        RpcUpdateVelocity(rb.velocity);
    }

    [Command]
    private void CmdStop()
    {
        // Arrêter le mouvement sur le serveur
        rb.velocity = Vector2.zero;
        RpcUpdateVelocity(Vector2.zero);
    }

    [ClientRpc]
    private void RpcUpdateVelocity(Vector2 newVelocity)
    {
        // Synchroniser la vitesse sur tous les clients
        rb.velocity = newVelocity;
    }

    [Command]
    private void CmdSetWalking(bool walking)
    {
        // Synchroniser l'état de marche sur le serveur
        isWalking = walking;
    }

    private void OnAnimationStateChanged(bool oldState, bool newState)
    {
        // Mettre à jour l'Animator lorsque l'état de marche change
        if (animator != null)
        {
            animator.SetBool("Player-Run", newState);
        }
    }

    public void SetCanMove(bool move)
    {
        // Autoriser ou interdire le mouvement
        canMove = move;
        if (!move)
        {
            CmdStop();
            CmdSetWalking(false); // Arrêter l'animation de marche
        }
    }
}