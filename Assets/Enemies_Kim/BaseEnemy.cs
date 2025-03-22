using Mirror;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseEnemy : NetworkBehaviour
{
    [SerializeField] protected float speed = 2f;
    protected Transform player;
    protected Rigidbody2D rb;

    public override void OnStartServer()
    {
        base.OnStartServer();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    [ServerCallback]
    public virtual void Move()
    {
        if (player != null && rb != null)
        {
            Vector2 direction = ((Vector2)player.position - rb.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    [ServerCallback]
    void FixedUpdate()
    {
        if (!isServer) return;
        Move();
    }

    [ServerCallback]
    void OnDisable()
    {
        if (rb != null)
            rb.velocity = Vector2.zero;
    }
}
