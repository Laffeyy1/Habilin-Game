using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private int damage;

    // Set the damage value for the bullet
    public void SetDamage(int damageValue)
    {
        damage = damageValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.GetHit(damage, gameObject);
            }

            Destroy(gameObject);
        }
    }
}
