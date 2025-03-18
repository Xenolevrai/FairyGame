using Mirror;
using UnityEngine;

public class BaseEnemy : NetworkBehaviour
{
    protected float speed = 0f;
    protected Transform player;

    public override void OnStartServer()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    [ServerCallback]
    public virtual void Move()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void Update()
    {
        if (isServer)
        {
            Move();
        }
    }
}
