using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloat : MonoBehaviour
{
    [SerializeField]
    private float amplitude = 0.2f; // The amplitude of the motion.
    [SerializeField]
    private float frequency = 1.0f; // The frequency of the motion.

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the new Y position using a sine wave.
        float newY = initialPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // Update the item's position.
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
