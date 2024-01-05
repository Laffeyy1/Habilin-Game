using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float delay = 2.0f; // Adjust the delay as needed

    void Start()
    {
        // Schedule the object for destruction after the specified delay
        Destroy(gameObject, delay);
    }
}
