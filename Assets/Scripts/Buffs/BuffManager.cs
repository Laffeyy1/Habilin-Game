using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private void Start()
    {
        // Example: Start regenerating health for the player
        StartHealthRegeneration(GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(), 60.0f, 10, 1.0f);
    }

    public void StartHealthRegeneration(Health healthComponent, float regenDuration, int healthToRegen, float regenInterval)
    {
        StartCoroutine(RegenerateHealth(healthComponent, regenDuration, healthToRegen, regenInterval));
    }

    private IEnumerator RegenerateHealth(Health healthComponent, float regenDuration, int healthToRegen, float regenInterval)
    {
        float regenTimer = 0.0f;

        while (regenTimer < regenDuration)
        {
            // Increment the health over time
            healthComponent.Heal(healthToRegen);

            // Wait for the next interval
            yield return new WaitForSeconds(regenInterval);

            // Update the timer
            regenTimer += regenInterval;
        }
    }
}
