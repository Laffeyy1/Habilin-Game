using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Lapu/SmashAbility")]
public class Smash : Ability
{
    public int damageAmount = 50;
    public float damageRadius = 5f;

    public GameObject skillFX; // Reference to your Particle System

    public override bool Activate(GameObject parent, int currentMana)
    {
        if (currentMana >= manaConsumption)
        {
            // Consume mana
            AudioManager am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            am.Skill1Lapu();

            currentMana -= manaConsumption;
            Debug.Log("SMASH!");
            if (skillFX != null)
            {
                Instantiate(skillFX, parent.transform.position, Quaternion.identity);
            }

            // Deal damage to all colliders within the damage radius
            Collider2D[] colliders = Physics2D.OverlapCircleAll(parent.transform.position, damageRadius);

            foreach (Collider2D col in colliders)
            {
                Health health = col.GetComponent<Health>();
                if (health != null)
                {
                    // Deal damage to the health component using GetHit method
                    health.GetHit(damageAmount, parent);
                }
            }
            return true; // Activation successful
        }
        else
        {
            return false; // Not enough mana
        }
    }
}
