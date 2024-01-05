using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockbackFeedack : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;

    public float strength = 16, delay = 0.15f;

    [SerializeField]
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    public GameObject bloodParticle;
    public Color hitColor = Color.white; // Color to apply when hit

    public UnityEvent OnBegin, OnDone;

    public void PlayFeedack(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();

        // Calculate direction and apply force
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb2d.AddForce(direction * strength, ForceMode2D.Impulse);

        // Change the sprite color temporarily
        StartCoroutine(ChangeSpriteColor(hitColor));
        Instantiate(bloodParticle, transform.position, Quaternion.identity);

        StartCoroutine(Reset());
    }

    private IEnumerator ChangeSpriteColor(Color targetColor)
    {
        // Save the original color
        Color originalColor = spriteRenderer.color;

        // Change the sprite color to the target color
        spriteRenderer.color = targetColor;

        // Wait for a short duration
        yield return new WaitForSeconds(delay);

        // Restore the original color
        spriteRenderer.color = originalColor;
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);

        // Reset the Rigidbody velocity
        rb2d.velocity = Vector3.zero;

        OnDone?.Invoke();
    }
}
