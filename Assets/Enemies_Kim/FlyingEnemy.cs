using UnityEngine;

public class FlyingEnemy : BaseEnemy
{
    public float separationDistance = 1.5f;
    public float hoverStrength = 0.3f;
    public float closeDistance = 0.5f;

    public override void Move()
    {
        if (player != null)
        {
            Vector2 targetPos = (Vector2)player.position;
            Vector2 separation = GetSeparationForce();
            Vector2 hoverOffset = new Vector2(
                Mathf.Sin(Time.time + gameObject.GetInstanceID() * 0.3f) * hoverStrength,
                Mathf.Cos(Time.time + gameObject.GetInstanceID() * 0.3f) * hoverStrength
            );

            Vector2 finalPosition = targetPos + hoverOffset + separation;
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > closeDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, finalPosition, speed * Time.deltaTime);
            }
        }
    }

    private Vector2 GetSeparationForce()
    {
        Vector2 separationForce = Vector2.zero;
        int nearbyEnemies = 0;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, separationDistance);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.CompareTag("Enemy"))
            {
                Vector2 awayFromEnemy = (Vector2)transform.position - (Vector2)collider.transform.position;
                separationForce += awayFromEnemy.normalized;
                nearbyEnemies++;
            }
        }

        if (nearbyEnemies > 0)
        {
            separationForce /= nearbyEnemies;
            separationForce *= 0.5f;
        }

        return separationForce;
    }
}
