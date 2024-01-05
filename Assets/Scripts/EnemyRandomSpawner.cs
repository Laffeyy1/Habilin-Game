using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs to spawn.
    public Transform[] spawnPoints; // Array of spawn points.
    public int numberOfEnemiesToSpawn = 5; // Number of enemies to spawn.

    private void Start()
    {
        SpawnRandomEnemies(numberOfEnemiesToSpawn);
    }

    private void SpawnRandomEnemies(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Randomly select an enemy prefab.
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject randomEnemyPrefab = enemyPrefabs[randomEnemyIndex];

            // Randomly select a spawn point.
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomSpawnIndex];

            // Instantiate the selected enemy at the spawn point.
            Instantiate(randomEnemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
