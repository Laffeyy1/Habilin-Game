using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Andres/Shoot")]
public class ShootSkill : Ability
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shootRange;
    [SerializeField] int damage;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLifetime;
    public GameObject ShootParticle;

    public override bool Activate(GameObject parent, int currentMana)
    {
        if (currentMana >= manaConsumption)
        {
            AudioManager am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            am.Skill2Andres();

            // Find all colliders in the shoot range with the "Enemy" or "Boss" tag
            Collider2D[] colliders = Physics2D.OverlapCircleAll(parent.transform.position, shootRange);
            Transform target = GetNearestEnemy(parent, colliders);

            if (ShootParticle != null)
            {
                // Instantiate the BuffParticle
                GameObject buffParticleInstance = Instantiate(ShootParticle, parent.transform.position, Quaternion.identity);

                // Make the BuffParticle a child of the parent
                buffParticleInstance.transform.SetParent(parent.transform);
            }

            if (target != null)
            {
                // Instantiate a bullet
                GameObject bullet = Instantiate(bulletPrefab, parent.transform.position, Quaternion.identity);

                // Calculate the shooting direction towards the target
                Vector3 shootDirection = (target.position - parent.transform.position).normalized;

                BulletScript bulletScript = bullet.GetComponent<BulletScript>();
                if (bulletScript != null)
                {
                    bulletScript.SetDamage(damage);
                }

                // Add force to the bullet's Rigidbody2D to make it move
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                if (bulletRb != null)
                {
                    bulletRb.velocity = shootDirection * bulletSpeed;
                }

                // Destroy the bullet after the specified lifetime
                Destroy(bullet, bulletLifetime);

                // Optionally, you can add more logic here based on your specific requirements

                return true; // Activation successful
            }
            else
            {
                Debug.Log("No enemies or bosses in range.");
                return false; // No target found
            }
        }
        else
        {
            Debug.Log("Not enough mana to activate the ability.");
            return false; // Not enough mana
        }
    }

    Transform GetNearestEnemy(GameObject parent, Collider2D[] colliders)
    {
        Transform nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy") || collider.CompareTag("Boss"))
            {
                float distance = Vector3.Distance(collider.transform.position, parent.transform.position);

                if (distance < nearestDistance)
                {
                    nearestEnemy = collider.transform;
                    nearestDistance = distance;
                }
            }
        }

        return nearestEnemy;
    }
}
