using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAI : MonoBehaviour
{
    public string playerTag = "Player"; // Reference to the player's tag
    public float followRange = 10f; // Distance at which AI starts following the player
    public float fleeRange = 5f; // Distance at which AI starts moving away from the player
    public float moveSpeed = 5f; // Speed at which the AI moves
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the follow range as a wire circle with a blue color
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, followRange);

        // Draw the flee range as a wire circle with a red color
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fleeRange);
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // If player is in follow range and not in flee range, follow the player
            if (distanceToPlayer <= followRange && distanceToPlayer > fleeRange)
            {
                FollowPlayer();
            }
            // If player is in flee range, move away from the player
            else if (distanceToPlayer <= fleeRange)
            {
                MoveAwayFromPlayer();
            }
        }
    }

    private void FollowPlayer()
    {
        // Calculate the direction towards the player
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;

        // Move the AI towards the player
        transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime);

        // Rotate the AI to face the player (optional)
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Implement shooting logic here
    }

    private void MoveAwayFromPlayer()
    {
        // Calculate the direction away from the player
        Vector2 directionAwayFromPlayer = (transform.position - player.transform.position).normalized;

        // Move the AI away from the player
        transform.Translate(directionAwayFromPlayer * moveSpeed * Time.deltaTime);

        // Rotate the AI to face away from the player (optional)
        float angle = Mathf.Atan2(directionAwayFromPlayer.y, directionAwayFromPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
