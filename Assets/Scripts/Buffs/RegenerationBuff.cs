using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationBuff : MonoBehaviour
{
    [SerializeField] private int healthToRegen = 10; // Amount of health to regenerate
    [SerializeField] private float regenInterval = 1.0f; // Time interval for regeneration in seconds
    [SerializeField] private float regenDuration = 60.0f; // Duration of regeneration in seconds

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Notify the RegenerationManager to start regeneration
            BuffManager regenerationManager = FindObjectOfType<BuffManager>();
            if (regenerationManager != null)
            {
                Health healthComponent = other.GetComponent<Health>();
                if (healthComponent != null)
                {
                    regenerationManager.StartHealthRegeneration(healthComponent, regenDuration, healthToRegen, regenInterval);
                }
            }
        }
    }
}