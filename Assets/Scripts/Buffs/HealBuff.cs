using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBuff : MonoBehaviour
{
    [SerializeField]
    private int healthToRestore = 10; // Amount of health to restore
    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Health healthComponent = other.GetComponent<Health>();

            if (healthComponent != null)
            {
                // Restore health to the Health component
                healthComponent.Heal(healthToRestore);
                audioManager.RestoreHealth();
            }
        }
    }
}
