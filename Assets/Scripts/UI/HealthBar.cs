using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text healthText;

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        UpdateHealthText(maxHealth, maxHealth); // Set initial health text.
    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        slider.value = currentHealth;
        UpdateHealthText(currentHealth, maxHealth);
    }

    private void UpdateHealthText(int currentHealth, int maxHealth)
    {
        healthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }
}
