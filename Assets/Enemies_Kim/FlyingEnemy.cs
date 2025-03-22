using Mirror;
using UnityEngine;

public class FlyingEnemy : BaseEnemy
{
    public float hoverStrength = 0.3f;
    public float attackDamage = 5f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;
    [SerializeField] private float flyingSpeed = 3f;


    public override void OnStartServer()
    {
        base.OnStartServer();
        speed = flyingSpeed;
    }

    [ServerCallback]
    public override void Move()
    {
        if (player != null)
        {
            Vector2 targetPos = player.position;
            Vector2 hoverOffset = new Vector2(Mathf.Sin(Time.time * 2) * hoverStrength, Mathf.Cos(Time.time * 2) * hoverStrength);
            transform.position = Vector2.MoveTowards(transform.position, targetPos + hoverOffset, speed * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isServer && collision.CompareTag("Player"))
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                collision.GetComponent<HealthSystem>()?.CmdTakeDamage(attackDamage);
                Debug.Log(gameObject.name + " is hovering and attacking!");
                lastAttackTime = Time.time;
            }
        }
    }
}
