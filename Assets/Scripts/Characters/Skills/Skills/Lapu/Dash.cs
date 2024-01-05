using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Lapu/Dash")]
public class Dash : Ability
{
    private CharacterMover characterMover;

    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;

    public GameObject DashParticle;
    public override bool Activate(GameObject parent, int currentMana)
    {
        if (currentMana >= manaConsumption)
        {
            AudioManager am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            am.Skill2Lapu();

            if (DashParticle != null)
            {
                // Instantiate the BuffParticle
                GameObject buffParticleInstance = Instantiate(DashParticle, parent.transform.position, Quaternion.identity);

                // Make the BuffParticle a child of the parent
                buffParticleInstance.transform.SetParent(parent.transform);
            }

            characterMover = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMover>();

            if (characterMover != null)
            {
                characterMover.dashSpeed = dashSpeed;
                characterMover.dashDuration = dashDuration;

                characterMover.Dash();
            }
            else
            {
                Debug.LogError("CharacterMover component not found on the player GameObject.");
            }

            return true;
        }
        else
        {
            Debug.Log("Not enough mana to activate the ability.");
            return false;
        }
    }
}
