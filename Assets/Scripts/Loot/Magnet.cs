using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private string collectibleTag = "Collectible";
    [SerializeField] private float magnetForce = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entered collider has the collectible tag
        if (other.CompareTag(collectibleTag))
        {
            Debug.Log("Coin is nearby");
            // Calculate the direction from the collectible to this collider and invert it
            Vector3 direction = transform.position - other.transform.position;

            // Normalize the direction to get a force vector
            Vector3 force = direction.normalized * magnetForce;

            // Apply the force to the collectible's Rigidbody2D (if it has one)
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }
}
