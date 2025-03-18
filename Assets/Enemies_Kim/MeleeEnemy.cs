using Mirror;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    public float attackDamage = 10f;
    [SerializeField] private float meleeSpeed = 3.5f;

    public override void OnStartServer()
    {
        base.OnStartServer(); 
        speed = meleeSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isServer && collision.gameObject.CompareTag("Player"))
        {
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.CmdTakeDamage(attackDamage);
            }
        }
    }
}
