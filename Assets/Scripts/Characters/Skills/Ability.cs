using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite icon;
    public int manaConsumption;
    public float cooldownTime;
    public float activateTime;

    public virtual bool Activate(GameObject parent, int currentMana)
    {
        if (currentMana >= manaConsumption)
        {
            // Consume mana
            currentMana -= manaConsumption;
            // Activate the ability
            // Your ability logic goes here
            return true; // Activation successful
        }
        else
        {
            return false; // Not enough mana
        }
    }
}
