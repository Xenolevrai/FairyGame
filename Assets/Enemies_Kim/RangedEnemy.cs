using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    public override void Move()
    {
        if (Vector2.Distance(transform.position, player.position) > 5f)
        {
            base.Move();
        }
    }
}
