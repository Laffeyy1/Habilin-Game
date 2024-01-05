using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Andres/BuffRage")]
public class BuffRage : Ability
{
    private BuffController buffController;

    public GameObject BuffParticle;

    public override bool Activate(GameObject parent, int currentMana)
    {
        if (currentMana >= manaConsumption)
        {
            AudioManager am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            am.Skill1Andres();

            if (BuffParticle != null)
            {
                // Instantiate the BuffParticle
                GameObject buffParticleInstance = Instantiate(BuffParticle, parent.transform.position, Quaternion.identity);

                // Make the BuffParticle a child of the parent
                buffParticleInstance.transform.SetParent(parent.transform);
            }

            // Get the BuffController component from the parent GameObject
            buffController = parent.GetComponent<BuffController>();

            // Activate the buff
            if (buffController != null)
            {
                buffController.ActivateBuff();
            }

            return true; // Activation successful
        }
        else
        {
            Debug.Log("Not enough mana to activate the ability.");
            return false; // Not enough mana
        }
    }
}