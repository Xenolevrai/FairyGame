using Mirror;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    [Header("Spawner Settings")]
    public GameObject meleeEnemyPrefab;
    public float spawnRate = 5f;
    public float activationRange = 10f;

    private Transform player;

    public override void OnStartServer()
    {
        // Delay slightly to give the player time to spawn
        InvokeRepeating(nameof(FindPlayerAndStartSpawning), 1f, 1f);
    }

    void FindPlayerAndStartSpawning()
    {
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
                InvokeRepeating(nameof(SpawnMeleeEnemyIfNearby), 1f, spawnRate);
                CancelInvoke(nameof(FindPlayerAndStartSpawning));
            }
        }
    }

    [Server]
    void SpawnMeleeEnemyIfNearby()
    {
        if (player == null || meleeEnemyPrefab == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= activationRange)
        {
            GameObject enemy = Instantiate(meleeEnemyPrefab, transform.position, Quaternion.identity);
            NetworkServer.Spawn(enemy);
            Debug.Log("Spawned MeleeEnemy!");
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, activationRange);
    }
#endif
}
