using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public float spawnRate = 600f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnRate);
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        int randomSpawn = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefabs[randomIndex], spawnPoints[randomSpawn].position, Quaternion.identity);
    }
}
