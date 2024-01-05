using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBoss : MonoBehaviour
{
    public string bossName;
    public TMP_Text bossNameHolder;
    public Slider bossHealthBar;

    public void SetMaxHealth(int health)
    {
        bossHealthBar.maxValue = health;
        bossHealthBar.value = health;
        SetName(bossName);
    }
    public void SetHealth(int health)
    {
        bossHealthBar.value = health;
    }

    public void SetName(string name)
    {
        bossNameHolder.text = name;
    }
}
