using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float speed = 10f; // Speed of the projectile
    public float lifetime = 2f; // Lifetime of the projectile in seconds
    public int damage = 1; // Damage inflicted by the projectile

    private float destroyTime; // Time at which the projectile should be destroyed

    void Start()
    {
        // Calculate the time at which the projectile should be destroyed
        destroyTime = Time.time + lifetime;
    }

    void Update()
    {
        // Check if the projectile should be destroyed due to its lifetime
        if (Time.time >= destroyTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the projectile collides with an enemy (or other objects you want to damage)
        if (other.CompareTag("Player"))
        {
            // Deal damage to the enemy (you can modify this logic as needed)
            Health healthComponent = other.GetComponent<Health>();
            if (healthComponent != null)
            {
                healthComponent.GetHit(damage, transform.gameObject);
            }
            // Destroy the projectile upon hitting the enemy
            Destroy(gameObject);
        }
    }
}
