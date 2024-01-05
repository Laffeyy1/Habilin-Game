using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    private DialogueManager dialogueManager;
    private bool isBlocked;

    Vector2 movement;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if (dialogueManager != null && dialogueManager.isDone)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // Reset the isBlocked flag
            isBlocked = false;
        }
        else
        {
            movement = Vector2.zero;
            // Check if isBlocked is false before logging
            if (!isBlocked)
            {
                isBlocked = true; // Set the flag to true to prevent spamming
            }
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
