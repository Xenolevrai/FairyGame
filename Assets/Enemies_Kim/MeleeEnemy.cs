using Mirror;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    public float attackDamage = 10f;
    public float attackCooldown = 1.5f; 
    private float lastAttackTime = 0f;
    [SerializeField] private float meleeSpeed = 3.5f;

    public override void OnStartServer()
    {
        base.OnStartServer(); 
        speed = meleeSpeed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isServer && collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= lastAttackTime + attackCooldown) 
            {
                HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
                if (playerHealth != null)
                {
                    playerHealth.CmdTakeDamage(attackDamage);
                    Debug.Log(gameObject.name + " attacked the player!");
                    lastAttackTime = Time.time; 
                }
            }
        }
    }
}
