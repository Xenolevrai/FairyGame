using Mirror;
using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    [SerializeField] private float rangedSpeed = 0f;


    public override void OnStartServer()
    {
        base.OnStartServer();
        speed = rangedSpeed;  
    }

    [ServerCallback]
    public override void Move()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > 5f)
        {
            base.Move(); 
        }
        else if (Time.time > lastAttackTime + attackCooldown)
        {
            ShootProjectile();
            lastAttackTime = Time.time;
            Debug.Log(gameObject.name + " fired a projectile at the player!");
        }
    }

    [Server]
    void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        NetworkServer.Spawn(projectile); 
        projectile.GetComponent<Rigidbody2D>().velocity = (player.position - projectileSpawnPoint.position).normalized * 5f;
    }
}
