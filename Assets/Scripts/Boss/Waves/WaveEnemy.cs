using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnemy : MonoBehaviour
{
    public int waveAmount;
    public GameObject[] enemyPrefabs;
    public int enemyAddPerWave;
    public GameObject bossPrefab;
    public Transform spawnLoc;

    private GameObject[] enemySpawned;
    private int currentWave = 0;
    private int counter;
    private bool[] hasBeenCounted;
    private bool hasBossSpawned = false;

    private void Start()
    {
        counter = 0;
        StartWave();
    }

    private void Update()
    {
        if (hasBossSpawned)
        {
            return;
        }
        if (counter == 0)
        {
            if (currentWave < waveAmount - 1)
            {
                StartWave();
            }
            else if (currentWave == waveAmount - 1 && !hasBossSpawned)
            {
                SpawnBoss();
                hasBossSpawned = true;
            }
        }
        for (int i = 0; i < enemySpawned.Length; i++)
        {
            if (enemySpawned[i] == null)
            {
                counter--;
                hasBeenCounted[i] = true;
            }
        }
    }

    private void StartWave()
    {
        currentWave++;
        enemySpawned = new GameObject[currentWave * enemyAddPerWave];
        counter = enemySpawned.Length; // Update the counter here

        for (int i = 0; i < currentWave * enemyAddPerWave; i++)
        {
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            Vector3 spawnPosition = spawnLoc.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            GameObject enemy = Instantiate(enemyPrefabs[randomEnemyIndex], spawnPosition, Quaternion.identity);
            enemySpawned[i] = enemy;
        }
    }

    private void SpawnBoss()
    {
        Vector3 bossSpawnPosition = spawnLoc.position;
        Instantiate(bossPrefab, bossSpawnPosition, Quaternion.identity);
    }
}
