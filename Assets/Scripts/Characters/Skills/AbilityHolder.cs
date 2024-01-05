using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class AbilityHolder : MonoBehaviour
{
    public Ability[] abilities; // Array of abilities
    private Ability currentAbility; // Currently equipped ability
    private int abilitySelectedIndex = 0;

    private float cooldownTimer;
    private Image[] cooldownImages;
    private float cooldownDuration;

    public int maxMana;
    public Image[] noMana;
    public int currentMana;
    private ManaBar manaBar;

    private int lvlAdd;
    private AbilityState state = AbilityState.Ready;

    /*    public event System.Action AbilityActivated;
        public event System.Action AbilityDeactivated;*/

    private enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }

    public InputAction abilityAction; // Reference to your ability activation Input Action
    void OnEnable()
    {
        abilitySelectedIndex = PlayerPrefs.GetInt("AbilitySelected", 0);

        // Set the current ability based on the selected index
        SetCurrentAbility(abilitySelectedIndex);

        abilityAction.Enable();
        abilityAction.started += _ => TryActivateAbility();

        manaBar = FindObjectOfType<ManaBar>();
        cooldownImages = GameObject.FindGameObjectsWithTag("CooldownButton").Select(go => go.GetComponent<Image>()).ToArray();
        noMana = GameObject.FindGameObjectsWithTag("NoManaImage").Select(go => go.GetComponent<Image>()).ToArray();
        lvlAdd = PlayerPrefs.GetInt("PlayerLevel");
        maxMana += lvlAdd;
        currentMana = maxMana;
        cooldownTimer = currentAbility.cooldownTime;
        foreach (Image cooldownImage in cooldownImages)
        {
            cooldownImage.fillAmount = 0;
        }

        foreach (Image noManaImage in noMana)
        {
            noManaImage.gameObject.SetActive(false);
        }

        manaBar.SetMaxMana(maxMana);

        abilityAction.Enable();
        abilityAction.started += _ => TryActivateAbility();
    }

    void OnDisable()
    {
        abilityAction.Disable();
        abilityAction.started -= _ => TryActivateAbility();
    }

    void Update()
    {
        if (state == AbilityState.Active)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                state = AbilityState.Ready;
                foreach (Image cooldownImage in cooldownImages)
                {
                    cooldownImage.fillAmount = 0;
                }
                if (currentMana < currentAbility.manaConsumption)
                {
                    foreach (Image noManaImage in noMana)
                    {
                        noManaImage.gameObject.SetActive(true);
                    }
                }
                else
                {
                    foreach (Image noManaImage in noMana)
                    {
                        noManaImage.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                foreach (Image cooldownImage in cooldownImages)
                {
                    cooldownImage.fillAmount = cooldownTimer / cooldownDuration;
                }
            }
        }
    }

    void SetCurrentAbility(int index)
    {
        if (index >= 0 && index < abilities.Length)
        {
            currentAbility = abilities[index];
            // Optionally, you can update UI or perform other actions based on the new current ability.
        }
        else
        {
            Debug.LogError("Invalid ability index: " + index);
        }
    }

    void TryActivateAbility()
    {
        if (state == AbilityState.Ready)
        {
            if (currentMana < currentAbility.manaConsumption)
            {
                // The player doesn't have enough mana, so show the "No Mana" images.
                foreach (Image noManaImage in noMana)
                {
                    noManaImage.gameObject.SetActive(true);
                }
            }
            else
            {
                // The player has enough mana to activate the ability, so hide the "No Mana" images.
                foreach (Image noManaImage in noMana)
                {
                    noManaImage.gameObject.SetActive(false);
                }

                bool activationSuccess = currentAbility.Activate(gameObject, currentMana);
                if (activationSuccess)
                {
                    state = AbilityState.Active;
                    cooldownTimer = currentAbility.cooldownTime;
                    cooldownDuration = currentAbility.cooldownTime;
                    currentMana -= currentAbility.manaConsumption;
                    UpdateManaSlider();
                }
            }
        }
    }

    void UpdateManaSlider()
    {
        if (manaBar != null)
        {
            manaBar.SetMana(currentMana, maxMana);
        }
    }

    public void RestoreMana(int amount)
    {
        currentMana += amount;
        currentMana = Mathf.Min(currentMana, maxMana);

        manaBar.SetMana(currentMana, maxMana);

        if (currentMana >= currentAbility.manaConsumption)
        {
            // The player doesn't have enough mana, so show the "No Mana" images.
            foreach (Image noManaImage in noMana)
            {
                noManaImage.gameObject.SetActive(false);
            }
        }
    }
}
