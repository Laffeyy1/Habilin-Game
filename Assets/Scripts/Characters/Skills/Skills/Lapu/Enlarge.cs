using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Lapu/Enlarge")]
public class Enlarge : Ability
{
    private WeaponParent weaponParent;

    [SerializeField] float weaponScale = 2f;
    [SerializeField] float scaleIncreaseFactor = 1.5f;
    [SerializeField] int damageMultiplier = 2;
    [SerializeField] float enlargeDuration = 1.0f;
    [SerializeField] float pauseDuration = 10.0f;
    [SerializeField] float returnDuration = 1.0f;

    public override bool Activate(GameObject parent, int currentMana)
    {
        if (currentMana >= manaConsumption)
        {
            AudioManager am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            am.Skill3Lapu();

            weaponParent = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponParent>();
            MonoBehaviour coroutineHandler = parent.GetComponent<MonoBehaviour>();

            if (weaponParent != null)
            {
                // Enlarge the weaponParent's properties
                coroutineHandler.StartCoroutine(ChangePropertiesOverTime(weaponScale, damageMultiplier, enlargeDuration));

                // Optionally, you can add other logic here based on the enlarged state
            }
            else
            {
                Debug.LogError("WeaponParent component not found on the player GameObject.");
            }

            return true;
        }
        else
        {
            Debug.Log("Not enough mana to activate the ability.");
            return false;
        }
    }

    IEnumerator ChangePropertiesOverTime(float scaleFactor, int damageFactor, float duration)
    {
        float elapsedTime = 0f;

        // Store the original values
        Vector3 originalScale = weaponParent.transform.localScale;
        float originalRadius = weaponParent.radius;
        int originalDamage = weaponParent.damage;

        // Enlarge
        while (elapsedTime < enlargeDuration)
        {
            weaponParent.transform.localScale = Vector3.Lerp(originalScale, originalScale * scaleFactor, elapsedTime / enlargeDuration);
            weaponParent.radius = Mathf.Lerp(originalRadius, originalRadius * scaleFactor, elapsedTime / enlargeDuration);
            weaponParent.damage = (int)Mathf.Lerp(originalDamage, originalDamage * damageFactor, elapsedTime / enlargeDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final values are set
        weaponParent.transform.localScale = originalScale * scaleFactor;
        weaponParent.radius = originalRadius * scaleFactor;
        weaponParent.damage = (int)(originalDamage * damageFactor);

        // Wait for at least 10 seconds
        yield return new WaitForSeconds(pauseDuration);

        // Return to original values
        elapsedTime = 0f;
        float returnDuration = 5.0f; // Adjust this duration as needed
        while (elapsedTime < returnDuration)
        {
            weaponParent.transform.localScale = Vector3.Lerp(originalScale * scaleFactor, originalScale, elapsedTime / returnDuration);
            weaponParent.radius = Mathf.Lerp(originalRadius * scaleFactor, originalRadius, elapsedTime / returnDuration);
            weaponParent.damage = (int)Mathf.Lerp(originalDamage * damageFactor, originalDamage, elapsedTime / returnDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final values are set
        weaponParent.transform.localScale = originalScale;
        weaponParent.radius = originalRadius;
        weaponParent.damage = originalDamage;
    }
}
