using Mirror;
using UnityEngine;

public class MovePlayer : NetworkBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 3f; // Vitesse de déplacement
    private Rigidbody2D rb; // Référence au Rigidbody2D
    private Animator animator; // Référence à l'Animator

    private Vector2 movement; // Direction du mouvement
    public bool canMove = true; // Autorisation de mouvement

    [Header("Animation Settings")]
    [SerializeField] private Transform spriteChild; // Référence à l'enfant du sprite (PourAnimation)

    [Header("Attack Settings")]
    [SerializeField] private float timeBetweenAttack = 1f; // Temps entre les attaques
    private float attackTime; // Temps de la prochaine attaque
    private bool isAttacking = false; // Indique si le joueur est en train d'attaquer

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
        // Seul le joueur local peut contrôler le mouvement et les animations
        if (!isLocalPlayer || !canMove) return;

        // Gestion de l'attaque
        if (Input.GetMouseButton(0)) // Clic gauche de la souris
        {
            if (Time.time >= attackTime && !isAttacking)
            {
                Attack();
                attackTime = Time.time + timeBetweenAttack;
            }
        }

        // Si le joueur est en train d'attaquer, ne pas bouger
        if (isAttacking) return;

        // Récupération des inputs pour le mouvement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalisation du vecteur de mouvement pour éviter les vitesses diagonales plus rapides
        movement = movement.normalized;

        // Mettre à jour les paramètres d'animation
        if (animator != null)
        {
            // Définir si le joueur est en mouvement
            bool isMoving = movement != Vector2.zero;
            animator.SetBool("IsMoving", isMoving);

            // Mettre à jour les directions
            if (isMoving)
            {
                animator.SetFloat("InputX", movement.x);
                animator.SetFloat("InputY", movement.y);

                // Enregistrer la dernière direction pour l'animation idle
                animator.SetFloat("lastInputX", movement.x);
                animator.SetFloat("lastInputY", movement.y);
            }

            // Retourner le sprite du joueur s'il va vers la gauche
            if (movement.x != 0 && spriteChild != null)
            {
                Vector3 newScale = spriteChild.localScale;
                newScale.x = Mathf.Abs(newScale.x) * (movement.x < 0 ? -1 : 1); // Inverser l'échelle X si nécessaire
                spriteChild.localScale = newScale;
            }
        }

        // Appliquer le mouvement
        if (movement != Vector2.zero)
        {
            CmdMove(movement);
        }
        else
        {
            CmdStop();
        }
    }

    private void Attack()
    {
        isAttacking = true; // Bloquer le mouvement

        if (animator != null)
        {
            animator.SetTrigger("attack"); // Déclencher l'animation d'attaque
        }

        // Synchroniser l'attaque sur le serveur
        CmdAttack();

        // Réactiver le mouvement après la durée de l'animation d'attaque
        Invoke("EndAttack", GetAttackAnimationDuration());
    }

    private void EndAttack()
    {
        isAttacking = false; // Réactiver le mouvement
    }

    private float GetAttackAnimationDuration()
    {
        // Récupérer la durée de l'animation d'attaque depuis l'Animator
        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("attack")) // Remplace "Attack" par le nom de ton animation d'attaque
            {
                return stateInfo.length; // Durée de l'animation
            }
        }
        return 0.5f; // Valeur par défaut si la durée n'est pas trouvée
    }

    [Command]
    private void CmdAttack()
    {
        isAttacking = true; // Synchroniser l'état d'attaque sur le serveur
        RpcAttack();
    }

    [ClientRpc]
    private void RpcAttack()
    {
        if (!isLocalPlayer) // Ne pas jouer l'animation d'attaque sur le joueur local (déjà jouée)
        {
            if (animator != null)
            {
                animator.SetTrigger("attack");
            }
        }
    }

    private void FixedUpdate()
    {
        // Appliquer le mouvement via Translate
        if (isLocalPlayer && canMove && movement != Vector2.zero && !isAttacking)
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

    public void SetCanMove(bool move)
    {
        // Autoriser ou interdire le mouvement
        canMove = move;
        if (!move)
        {
            CmdStop();
        }
    }
}