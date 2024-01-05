using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float maxSpeed = 2, acceleration = 50, deaceleration = 100;
    public float dashSpeed = 10f; // Adjust the dash speed as needed
    public float dashDuration = .5f;
    private bool isDashing = false;

    [SerializeField]
    private float currentSpeed = 0;
    private Vector2 oldMovementInput;
    public Vector2 MovementInput { get; set; }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            // If dashing, use dashSpeed instead of regular acceleration
            rb2d.velocity = oldMovementInput * dashSpeed;
        }
        else
        {
            if (MovementInput.magnitude > 0 && currentSpeed >= 0)
            {
                oldMovementInput = MovementInput;
                currentSpeed += acceleration * maxSpeed * Time.deltaTime;
            }
            else
            {
                currentSpeed -= deaceleration * maxSpeed * Time.deltaTime;
            }

            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            rb2d.velocity = oldMovementInput * currentSpeed;
        }
    }

    // Call this method to trigger a dash
    public void Dash()
    {
        if (!isDashing)
        {
            StartCoroutine(StartDash());
        }
    }

    IEnumerator StartDash()
    {
        isDashing = true;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }
}
