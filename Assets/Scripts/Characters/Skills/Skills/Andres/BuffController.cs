using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    public float initialBuffDuration = 5f; // Duration of the initial buff in seconds
    public float decayDuration = 2f; // Duration of the decay in seconds

    public int healthBuffRegen = 5; // Health buff amount
    public float speed = 6; // Speed buff amount

    private Health healthComponent; // Reference to the health component
    private CharacterMover moverComponent; // Reference to the character mover component

    private int defautlHealth;
    private float defaultSpeed;

    private float buffTimer; // Timer to track the duration of the buff

    private bool isBuffActive = false; // Flag to indicate if the buff is active

    private void Start()
    {
        // Get references to the Health and CharacterMover components
        healthComponent = GetComponent<Health>();
        moverComponent = GetComponent<CharacterMover>();

        // Set the initial timer value to the initial buff duration
        buffTimer = initialBuffDuration;

        defautlHealth = healthComponent.maxHealth;
        defaultSpeed = moverComponent.maxSpeed;
    }

    private void Update()
    {
        if (isBuffActive)
        {
            // Decrease the timer
            buffTimer -= Time.deltaTime;

            if (buffTimer <= 0)
            {
                // Buff duration has ended, apply decay
                ApplyDecay();
            }
        }
    }

    public void ActivateBuff()
    {
        if (!isBuffActive)
        {
            // Apply the buff
            ApplyBuff();

            // Start the timer
            buffTimer = initialBuffDuration;
            isBuffActive = true;
        }
    }

    private void ApplyBuff()
    {
        // Apply health and speed buffs
        if (healthComponent != null)
        {
            healthComponent.currentHealth += healthBuffRegen;
        }

        if (moverComponent != null)
        {
            moverComponent.maxSpeed = speed;
        }
    }

    private void ApplyDecay()
    {
        // Decay health and speed back to their default values
        if (healthComponent != null)
        {
            healthComponent.currentHealth -= healthBuffRegen;
        }

        if (moverComponent != null)
        {
            // Reset speed to the default value (you should have a way to obtain this)
            moverComponent.maxSpeed = defaultSpeed;
        }

        isBuffActive = false; // Reset the buff state
    }
}
