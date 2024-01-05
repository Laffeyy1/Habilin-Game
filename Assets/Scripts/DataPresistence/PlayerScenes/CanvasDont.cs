using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDont : MonoBehaviour
{
    public static CanvasDont instance; // Change this to static

    private Vector3 initialPosition; // Store the initial position of the player

    private void Awake()
    {
        

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Store the initial position when the script is first instantiated
        initialPosition = new Vector3(0f, 0f, 0f); // Set the initial position to (0, 0, 0)
        transform.position = initialPosition; // Set the player's position to the initial position
    }
}
