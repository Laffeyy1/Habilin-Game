using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab; // The prefab of the projectile to shoot
    public string playerTag = "Player"; // Tag of the player GameObject
    public float fireRate = 1f; // Rate of fire (shots per second)
    public int projectileCount = 1; // Number of projectiles to shoot at once
    public float angleSteep = 45f;
    private float nextFireTime; // Time for the next shot
    private Transform player; // Reference to the player's transform

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag(playerTag).transform;

        if (player == null)
        {
            Debug.LogError("Player not found with tag: " + playerTag);
        }
    }

    void Update()
    {
        // Check if the player is found and it's time to fire again
        if (player != null && Time.time >= nextFireTime)
        {
            // Fire multiple projectiles towards the player
            for (int i = 0; i < projectileCount; i++)
            {
                ShootProjectile(i);
            }

            // Calculate the time for the next shot
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void ShootProjectile(int index)
    {
        // Calculate the direction towards the player with angle offset
        float angleOffset = (index - (projectileCount - 1) / 2f) * angleSteep;
        Vector2 direction = Quaternion.Euler(0, 0, angleOffset) * (player.position - transform.position).normalized;

        // Create a new instance of the projectile prefab at the enemy's position
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        // Access the projectile's rigidbody (assuming it has one) to apply force
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Debug the direction for troubleshooting
            Debug.Log("Direction: " + direction);
            // Adjust the force and direction as needed
            rb.velocity = direction * 10f;
        }
    }
}
