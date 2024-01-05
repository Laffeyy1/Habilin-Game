using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : MonoBehaviour
{
    [SerializeField]
    private int healthToAdd = 10; // Amount of health to add

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Health healthComponent = other.GetComponent<Health>();

            if (healthComponent != null)
            {
                // Add health to the Health component
                healthComponent.AddHealth(healthToAdd);
            }
        }
    }
}
