using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text manaText;

    public void SetMaxMana(int maxMana)
    {
        slider.maxValue = maxMana;
        slider.value = maxMana;
        UpdateManaText(maxMana, maxMana); // Set initial mana text.
    }

    public void SetMana(int currentMana, int maxMana)
    {
        slider.value = currentMana;
        UpdateManaText(currentMana, maxMana);
    }

    private void UpdateManaText(int currentMana, int maxMana)
    {
        manaText.text = currentMana.ToString() + "/" + maxMana.ToString();
    }
}
