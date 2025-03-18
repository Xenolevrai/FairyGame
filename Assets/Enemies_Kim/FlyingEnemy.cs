using Mirror;
using UnityEngine;

public class FlyingEnemy : BaseEnemy
{
    public float hoverStrength = 0.3f;
    public float attackDamage = 5f;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isServer && collision.CompareTag("Player"))
        {
            collision.GetComponent<HealthSystem>()?.CmdTakeDamage(attackDamage);
            Debug.Log(gameObject.name + " attacked the player!");
        }
    }
}
