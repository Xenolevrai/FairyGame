using Mirror;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    [System.Serializable]
    public struct SpawnPointData
    {
        public Transform spawnPoint; 
        public GameObject enemyPrefab; 
    }

    public SpawnPointData[] spawnPoints; 
    public float spawnRate = 10f; 

    public override void OnStartServer()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnRate);
    }

    [Server]
    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        foreach (SpawnPointData spawnData in spawnPoints)
        {
            if (spawnData.spawnPoint != null && spawnData.enemyPrefab != null)
            {
                GameObject enemy = Instantiate(spawnData.enemyPrefab, spawnData.spawnPoint.position, Quaternion.identity);
                NetworkServer.Spawn(enemy); 
            }
        }
    }
}
