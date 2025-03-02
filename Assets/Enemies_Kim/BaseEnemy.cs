using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float speed = 2f;
    public int health = 100;
    protected Transform player;
    protected Rigidbody2D rb;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public virtual void Move()
    {
        if (player != null && rb != null)
        {
            Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        Move();
    }
}
