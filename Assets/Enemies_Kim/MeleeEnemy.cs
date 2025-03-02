using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    public float attackRange = 1f;

    public override void Move()
    {
        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            base.Move();
        }
    }
}
