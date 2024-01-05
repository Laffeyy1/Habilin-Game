using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterLevel : MonoBehaviour
{
    public GameObject barrier; // Reference to the barrier GameObject
    public MonoBehaviour enemySpawner; // Reference to the enemy spawner script
    public Transform enemySpawnPoint; // Spawn point for enemies

    private bool barrierEnabled = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!barrierEnabled)
            {
                // Enable the barrier
                barrier.SetActive(true);
                barrierEnabled = true;

                // Enable the enemy spawner script
                enemySpawner.enabled = true;

                // Spawn enemies
                SpawnEnemies();
            }
        }
    }

    private void SpawnEnemies()
    {
        // You can optionally add enemy spawning logic here, or leave it to the enemy spawner script.
    }
}
