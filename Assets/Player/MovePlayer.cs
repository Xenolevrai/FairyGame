using Mirror;
using UnityEngine;

public class MovePlayer : NetworkBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;
    private Vector2 dir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D non trouvé sur l'objet.");
        }
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        if (dir != Vector2.zero)
        {
            CmdMove(dir);
        }
        else
        {
            CmdStop();
        }
    }

    [Command]
    private void CmdMove(Vector2 direction)
    {
        if (rb == null) return;

        rb.velocity = direction * speed;
        RpcUpdateVelocity(rb.velocity);
    }

    [Command]
    private void CmdStop()
    {
        if (rb == null) return;

        rb.velocity = Vector2.zero;
        RpcUpdateVelocity(Vector2.zero);
    }

    [ClientRpc]
    private void RpcUpdateVelocity(Vector2 newVelocity)
    {
        if (rb == null) return;

        rb.velocity = newVelocity;
    }
}