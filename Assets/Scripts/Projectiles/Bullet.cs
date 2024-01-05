using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 1f;  // Defines how long before the bullet is destroyed
    public float speed = 1f;

    private Vector2 spawnPoint;
    private Vector2 direction;  // Added direction vector
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > bulletLife)
        {
            Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime;
            Vector2 newPosition = Movement(timer);
            transform.position = newPosition;
        }
    }

    // Set the direction of the bullet
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;  // Normalize the direction vector
    }

    private Vector2 Movement(float timer)
    {
        // Move the bullet in the specified direction with speed
        Vector2 newPosition = spawnPoint + direction * speed * timer;
        return newPosition;
    }
}
