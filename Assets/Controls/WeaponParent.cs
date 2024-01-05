using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer characterRederer, weaponRenderer;
    public Vector2 Poiterposition { get; set; }

    public Animator animator;
    public float delay = 0.3f;
    private bool attackBlocked;
    public int damage = 1;
    public float knockback = 16f;
    public bool IsAttacking { get; private set; }

    public Transform circleOrigin;
    public float radius;

    private Vector2 lastLookDirection;
    private int difficultyMultiplier = 0;
    private int levelMultiplier = 0;
    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Difficulty"))
        {
            difficultyMultiplier = PlayerPrefs.GetInt("Difficulty");
            levelMultiplier = PlayerPrefs.GetInt("Level");
        }
        
        characterRederer = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SpriteRenderer>();
        

        if (CompareTag("Enemy"))
        {
            damage += difficultyMultiplier + levelMultiplier;
        }
    }

    private void Update()
    {
        if (IsAttacking)
            return;

        if (characterRederer == null)
        {
            IsAttacking = true;
            return;
        }

        Vector2 direction = Poiterposition;

        // Check if the direction is zero (character not moving)
        if (direction != Vector2.zero)
        {
            lastLookDirection = direction.normalized; // Update lastLookDirection
        }

        // Use the last look direction to set the right direction of the Transform
        transform.right = lastLookDirection;

        Vector2 scale = transform.localScale;

        transform.localScale = scale; 

        if(transform.eulerAngles.z > 0 && transform.eulerAngles.z < 90)
        {
            weaponRenderer.sortingOrder = characterRederer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRederer.sortingOrder + 1;
        }
    }

    public void Attack()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            KnockbackFeedack knockbackFeedack;
            if(knockbackFeedack = collider.GetComponent<KnockbackFeedack>())
            {
                knockbackFeedack.strength = knockback;
            }
            Health health;
            if (health = collider.GetComponent<Health>())
            {
                health.GetHit(damage, transform.root.gameObject);
            }
        }
    }

    public void SetPoiterposition(Vector2 position)
    {
        Poiterposition = position;
    }
}
