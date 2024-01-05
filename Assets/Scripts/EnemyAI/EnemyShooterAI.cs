using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyShooterAI : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack;

    [SerializeField]
    private string playerTag = "Player"; // Tag of the player GameObject

    [SerializeField]
    private float chaseDistanceThreshold = 3, attackDistanceThreshold = 0.8f;
    private GameObject AggroDistance;
    private GameObject AttackDistance;
    [SerializeField]
    private float attackDelay = 1;
    private float passedTime = 1;

    private Transform player; // Reference to the player's Transform

    private void Start()
    {
        // Find the player GameObject by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player not found. Make sure the player GameObject has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(player.position, transform.position);

        if (distance < chaseDistanceThreshold)
        {
            OnPointerInput?.Invoke(player.position);
            if (distance <= attackDistanceThreshold)
            {
                //attack behaviour
                OnMovementInput?.Invoke(Vector2.zero);
                if (passedTime >= attackDelay)
                {
                    passedTime = 0;
                    OnAttack?.Invoke();
                }
            }
            else
            {
                //chasing the player
                Vector2 direction = player.position - transform.position;
                OnMovementInput?.Invoke(direction.normalized);
            }
        }

        if (passedTime < attackDelay)
        {
            passedTime += Time.deltaTime;
        }
    }
}
