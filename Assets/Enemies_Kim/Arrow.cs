using Mirror;
using UnityEngine;

public class EnemyProjectile : NetworkBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isServer && collision.CompareTag("Player"))
        {
            collision.GetComponent<HealthSystem>()?.CmdTakeDamage(damage);
            NetworkServer.Destroy(gameObject);
        }
    }
}
