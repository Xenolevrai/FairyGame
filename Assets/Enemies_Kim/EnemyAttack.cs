using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public enum AttackType { Melee, Ranged, Flying }
    public AttackType attackType;

    public float attackDamage = 10f;
    public float attackRange = 1f;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    private Transform player;
    private HealthSystem playerHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            playerHealth = player.GetComponent<HealthSystem>();
        }
    }

    void Update()
    {
        if (player == null || playerHealth == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        switch (attackType)
        {
            case AttackType.Melee:
                if (distanceToPlayer <= attackRange) MeleeAttack();
                break;

            case AttackType.Ranged:
                if (distanceToPlayer <= attackRange) RangedAttack();
                break;

            case AttackType.Flying:
                FlyingAttack(distanceToPlayer);
                break;
        }
    }

    private void MeleeAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            playerHealth.CmdTakeDamage(attackDamage); // Utilise CmdTakeDamage au lieu de TakeDamage
            lastAttackTime = Time.time;
            Debug.Log(gameObject.name + " performed a melee attack!");
        }
    }

    private void RangedAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            if (projectilePrefab != null && projectileSpawnPoint != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = (player.position - projectileSpawnPoint.position).normalized;
                    rb.velocity = direction * 5f;
                }
            }
            lastAttackTime = Time.time;
            Debug.Log(gameObject.name + " fired a projectile!");
        }
    }

    private void FlyingAttack(float distanceToPlayer)
    {
        if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
        {
            playerHealth.CmdTakeDamage(attackDamage); // Utilise CmdTakeDamage au lieu de TakeDamage
            lastAttackTime = Time.time;
            Debug.Log(gameObject.name + " is hovering and dealing damage!");
        }
    }
}